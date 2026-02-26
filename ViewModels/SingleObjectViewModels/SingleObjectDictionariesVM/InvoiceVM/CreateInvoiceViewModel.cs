using BusinessLogic.Models.EntitiesForView;
using BusinessLogic.Models.Validators;
using BusinessLogic.Services.Printing;
using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.InvoiceItemVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderItemVM;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.InvoiceVM
{
    public class CreateInvoiceViewModel : CreateViewModel<Invoice>
    {
        public CreateInvoiceViewModel() : base("Add Invoice")
        {
            SelectTemplateCommand = new RelayCommand(SelectTemplate);
            AddInvoiceItemCommand = new RelayCommand(AddInvoiceItem);
            RemoveInvoiceItemCommand = new RelayCommand<CreateInvoiceItemViewModel>(RemoveInvoiceItem);
            CopyOrderItemsCommand = new RelayCommand(CopyOrderItemsToInvoice);
            InvoiceItems.CollectionChanged += InvoiceItems_CollectionChanged;
        }
        public ICommand AddInvoiceItemCommand { get; }
        public ICommand RemoveInvoiceItemCommand { get; }
        public ICommand CopyOrderItemsCommand { get; }
        public ICommand SelectTemplateCommand { get; }

        

        protected override void InitializeNewItem()
        {
            item.CreatedAt = DateTime.Now;
            item.InvoiceDate = DateTime.Now;
            item.DueDate = DateTime.Now.AddDays(7);
            item.IsActive = true;

            item.PaymentMethod = sharedData_Entities.PaymentMethods
                .Where(p => p.IsActive)
                .Select(p => p.PaymentMethodId)
                .First();
        }


        public string? InvoiceNumber
        {
            get => item.InvoiceNumber;
            set
            {
                item.InvoiceNumber = value;
                OnPropertyChanged(() => InvoiceNumber);
            }
        }

        public DateTime? InvoiceDate
        {
            get => item.InvoiceDate;
            set
            {
                if (item.InvoiceDate == value)
                    return;

                var error = DateValidator.DoDatesMatch(value, DueDate, "Invoice Date", "Due Date");
                if (error != null)
                {
                    System.Windows.MessageBox.Show(error);
                    return;
                }

                item.InvoiceDate = value;
                OnPropertyChanged(() => InvoiceDate);

                if (IsPaid == true)
                    PaidAt = value;
            }
        }

        // READ‑ONLY — liczone w bazie przez trigger
        public decimal TotalNet => item.TotalNet;
        public decimal? TotalVat => item.TotalVat;
        public decimal? TotalGross => item.TotalGross;

        public bool? IsPaid
        {
            get => item.IsPaid;
            set
            {
                if (item.IsPaid.GetValueOrDefault() == value.GetValueOrDefault())
                    return;


                item.IsPaid = value;
                OnPropertyChanged(() => IsPaid);

                if (value == true)
                    PaidAt = InvoiceDate;
                else
                    PaidAt = null;

            }
        }

        public string? Notes
        {
            get => item.Notes;
            set
            {
                item.Notes = value;
                OnPropertyChanged(() => Notes);
            }
        }

        public DateTime? CreatedAt
        {
            get => item.CreatedAt;
            set
            {
                if (item.CreatedAt != value)
                {
                    item.CreatedAt = value;
                    OnPropertyChanged(() => CreatedAt);
                }
            }
        }

        public DateTime? DueDate
        {
            get => item.DueDate;
            set
            {
                var error = DateValidator.DoDatesMatch(InvoiceDate, value, "Invoice Date", "Due Date");
                if (error != null)
                {
                    System.Windows.MessageBox.Show(error);
                    return;
                }

                item.DueDate = value;
                OnPropertyChanged(() => DueDate);
            }
        }

        public DateTime? PaidAt
        {
            get => item.PaidAt;
            set
            {
                item.PaidAt = value;
                OnPropertyChanged(() => PaidAt);
            }
        }

        public int OrderId
        {
            get => item.OrderId;
            set
            {
                if (item.OrderId == value)
                    return;

                item.OrderId = value;
                OnPropertyChanged(() => OrderId);
                LoadOrderItems(value);
            }
        }

        public int PaymentMethod
        {
            get => item.PaymentMethod;
            set
            {
                if (item.PaymentMethod != value)
                {
                    item.PaymentMethod = value;
                    OnPropertyChanged(() => PaymentMethod);
                }
            }
        }
        private string? _selectedTemplatePath;

        public string? SelectedTemplatePath
        {
            get => _selectedTemplatePath;
            set
            {
                _selectedTemplatePath = value;
                OnPropertyChanged(() => SelectedTemplatePath);
            }
        }

        public string? SelectedTemplateFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SelectedTemplatePath))
                    return null;

                return Path.GetFileName(SelectedTemplatePath);
            }
        }


        public decimal PreviewTotalNet =>
    InvoiceItems.Sum(i => i.PreviewLineNet);

        public decimal PreviewTotalVat =>
            InvoiceItems.Sum(i => i.PreviewLineVat);

        public decimal PreviewTotalGross =>
            InvoiceItems.Sum(i => i.PreviewLineGross);



        public IQueryable<KeyAndValue> OrderComboBoxItems =>
    sharedData_Entities.Orders
        .Where(o => o.IsActive == true)
        .Select(o => new KeyAndValue
        {
            Key = o.OrderId,
            Value = "Order #" + o.OrderId
        })
        .ToList()
        .AsQueryable();

        public IQueryable<KeyAndValue> PaymentMethodComboBoxItems =>
    sharedData_Entities.PaymentMethods
        .Where(p => p.IsActive == true)
        .Select(p => new KeyAndValue
        {
            Key = p.PaymentMethodId,
            Value = p.Name
        })
        .ToList()
        .AsQueryable();


        public IQueryable<KeyAndValue> ProductComboBoxItems =>
    sharedData_Entities.Products
        .Where(p => p.IsActive == true)
        .Select(p => new KeyAndValue
        {
            Key = p.ProductId,
            Value = p.Name
        })
        .ToList()
        .AsQueryable();


        public ObservableCollection<OrderItemPreviewViewModel> SelectedOrderItems { get; } = new();
        public ObservableCollection<CreateInvoiceItemViewModel> InvoiceItems { get; } = new();

        private void InvoiceItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (CreateInvoiceItemViewModel vm in e.NewItems)
                    vm.PropertyChanged += InvoiceItem_PropertyChanged;

            if (e.OldItems != null)
                foreach (CreateInvoiceItemViewModel vm in e.OldItems)
                    vm.PropertyChanged -= InvoiceItem_PropertyChanged;

            RefreshTotals();
        }

        private void InvoiceItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is
                nameof(CreateInvoiceItemViewModel.PreviewLineNet) or
                nameof(CreateInvoiceItemViewModel.PreviewLineVat) or
                nameof(CreateInvoiceItemViewModel.PreviewLineGross))
            {
                RefreshTotals();
            }
        }


        private void RefreshTotals()
        {
            //do podgladu w UI
            OnPropertyChanged(() => PreviewTotalNet); 
            OnPropertyChanged(() => PreviewTotalVat); 
            OnPropertyChanged(() => PreviewTotalGross);

            //do zapisu w bazie
            OnPropertyChanged(() => TotalNet);
            OnPropertyChanged(() => TotalVat);
            OnPropertyChanged(() => TotalGross);
        }

        private void AddInvoiceItem()
            => InvoiceItems.Add(new CreateInvoiceItemViewModel(new InvoiceItem()));

        private void RemoveInvoiceItem(CreateInvoiceItemViewModel vm)
        {
            if (vm != null)
                InvoiceItems.Remove(vm);
        }

        private void LoadOrderItems(int orderId)
        {
            SelectedOrderItems.Clear();

            var orderItems = sharedData_Entities.OrderItems
                .Include(oi => oi.CelebrationCakeNavigation)
                .Include(oi => oi.CelebrationCakeSizeNavigation)
                .Where(oi => oi.OrderId == orderId)
                .ToList();

            foreach (var item in orderItems)
                SelectedOrderItems.Add(new OrderItemPreviewViewModel(item));
        }


        private void RecalculateTotals()
        {
            item.TotalNet = InvoiceItems.Sum(i => i.PreviewLineNet);
            item.TotalVat = InvoiceItems.Sum(i => i.PreviewLineVat);
            item.TotalGross = InvoiceItems.Sum(i => i.PreviewLineGross);
        }

        private void CopyOrderItemsToInvoice()
        {
            if (!SelectedOrderItems.Any())
                return;

            InvoiceItems.Clear();

            const decimal vatRate = 23m;
            const decimal vatMultiplier = 1.23m;

            foreach (var orderItem in SelectedOrderItems)
            {
                var product = sharedData_Entities.Products
                    .FirstOrDefault(p =>
                        p.CelebrationCakeId == orderItem.CelebrationCakeId &&
                        p.IsActive == true);

                if (product == null)
                    continue;

                var netUnitPrice = Math.Round(
                    orderItem.UnitPrice / vatMultiplier,
                    2,
                    MidpointRounding.AwayFromZero);

                var invoiceItem = new InvoiceItem
                {
                    ProductId = product.ProductId,
                    Quantity = orderItem.Quantity,
                    UnitPrice = netUnitPrice,
                    VatRate = vatRate,
                    IsActive = true
                };

                InvoiceItems.Add(new CreateInvoiceItemViewModel(invoiceItem));
            }
        }

        private void SelectTemplate()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Word templates (*.docx)|*.docx",
                Title = "Select invoice template"
            };

            if (dialog.ShowDialog() == true)
            {
                SelectedTemplatePath = dialog.FileName;
            }
        }



        public override void Save()
        {
            //Walidacja – czy faktura ma zamowieneie
            if (item.OrderId <= 0)
            {
                System.Windows.MessageBox.Show("Select an order before saving the invoice.");
                return;
            }

            // Zapis faktury (InvoiceId)
            sharedData_Entities.Add(item);
            sharedData_Entities.SaveChanges();

            // zapis pozycji faktury
            foreach (var vm in InvoiceItems)
            {
                vm.item.InvoiceId = item.InvoiceId;
                item.InvoiceItems.Add(vm.item);
            }

            sharedData_Entities.SaveChanges();

            // przeliczenie sum
            RecalculateTotals();
            sharedData_Entities.SaveChanges();

            // budowa modelu wydruku
            var builder = new InvoicePrintModelBuilder(sharedData_Entities);
            var printModel = builder.Build(item.InvoiceId);

            // sciezki do plikow
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            // domyslny szablon
            var templatePath = string.IsNullOrWhiteSpace(SelectedTemplatePath)
                ? Path.Combine(basePath, "Templates", "InvoiceTemplate.docx")
                : SelectedTemplatePath;

            // folder na faktury
            var invoicesPath = Path.Combine(basePath, "Invoices");
            Directory.CreateDirectory(invoicesPath);

            var safeInvoiceNumber = item.InvoiceNumber
                .Replace("/", "_")
                .Replace("\\", "_");

            var outputPath = Path.Combine(
                invoicesPath,
                $"Invoice_{safeInvoiceNumber}.docx");

            // generowanie dokumentu
            var engine = new WordTemplateEngine();
            engine.GenerateInvoice(printModel, templatePath, outputPath);

            //konwertowanie do PDF
            var pdfPath = Path.ChangeExtension(outputPath, ".pdf");

            var pdfConverter = new WordToPdfConverter();
            pdfConverter.Convert(outputPath, pdfPath);

            // wywolanie metody z klasy bazowej
            base.Save();
        }



    }
}
