using BusinessLogic.Models.EntitiesForView;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System.ComponentModel;
using System.Linq;
using BusinessLogic.Models.Validators;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.InvoiceItemVM
{
    public class CreateInvoiceItemViewModel : CreateViewModel<InvoiceItem>
    {
        public CreateInvoiceItemViewModel()
            : base("Add Invoice Item")
        {
            InitializeNewItem();
        }

        public CreateInvoiceItemViewModel(InvoiceItem existingItem)
            : base("Add Invoice Item")
        {
            item = existingItem;
            InitializeNewItem();
        }

        protected override void InitializeNewItem()
        {
            item.IsActive = true;
            item.Quantity ??= 1;
            item.VatRate ??= 23;
        }

        public int ProductId
        {
            get => item.ProductId;
            set
            {
                if (item.ProductId != value)
                {
                    item.ProductId = value;
                    OnPropertyChanged(() => ProductId);

                    // powoduje przeliczenie linii
                    RefreshComputed();
                }
            }
        }

        public int? Quantity
        {
            get => item.Quantity;
            set
            {
                var error = BusinessValidator.IsGreaterThanZero(value, "Quantity");
                if (error != null)
                {
                    System.Windows.MessageBox.Show(error);
                    return;
                }

                item.Quantity = value;
                OnPropertyChanged(() => Quantity);
                RefreshComputed();
            }
        }

        public decimal UnitPrice
        {
            get => item.UnitPrice;
            set
            {
                item.UnitPrice = value;
                OnPropertyChanged(() => UnitPrice);
                RefreshComputed();
            }
        }

        public decimal? VatRate
        {
            get => item.VatRate;
            set
            {
                item.VatRate = value;
                OnPropertyChanged(() => VatRate);
                RefreshComputed();
            }
        }
        //tylko do podglądu w UI
        public decimal PreviewLineNet =>
    (item.Quantity ?? 0) * item.UnitPrice;

        public decimal PreviewLineVat =>
            PreviewLineNet * (item.VatRate ?? 0) / 100;

        public decimal PreviewLineGross =>
            PreviewLineNet + PreviewLineVat;



        // READ‑ONLY — liczone w SQL
        public decimal LineNet => item.LineNet;
        public decimal LineVat => item.LineVat;
        public decimal LineGross => item.LineGross;

        private void RefreshComputed()
        {
            OnPropertyChanged(() => PreviewLineNet);
            OnPropertyChanged(() => PreviewLineVat);
            OnPropertyChanged(() => PreviewLineGross);
        }

    }

}