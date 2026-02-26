using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.InvoiceVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllInvoicesViewModel : AllViewModel<InvoiceViewModel>
    {
        #region Constructor
        public AllInvoicesViewModel()
            : base("Invoices")
        {
            /*
            DetailsCommand = new RelayCommand<InvoiceViewModel>(ShowDetails);
            UpdateCommand = new RelayCommand<InvoiceViewModel>(ShowUpdate);
            DeleteCommand = new RelayCommand<InvoiceViewModel>(ShowDelete);
            */
            Messenger.Default.Register<string>(this, "Invoice", msg => { Load(); });
        }
        #endregion

        #region Properties
        private InvoiceViewModel _SelectedInvoice;
        public InvoiceViewModel SelectedInvoice
        {
            get => _SelectedInvoice;
            set
            {
                if (value != _SelectedInvoice)
                {
                    _SelectedInvoice = value;
                    Messenger.Default.Send(_SelectedInvoice);
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand<InvoiceViewModel> DetailsCommand { get; set; }
        public RelayCommand<InvoiceViewModel> UpdateCommand { get; set; }
        public RelayCommand<InvoiceViewModel> DeleteCommand { get; set; }
        /*
        private void ShowDetails(InvoiceViewModel invoice)
        {
            if (invoice == null) return;

            var displayVM = new DisplayInvoiceViewModel();
            displayVM.Load(invoice.InvoiceId);

            Messenger.Default.Send(displayVM, "InvoiceDisplay");
        }

        private void ShowUpdate(InvoiceViewModel invoice)
        {
            if (invoice == null) return;

            var updateVM = new UpdateInvoiceViewModel();
            updateVM.Load(invoice.InvoiceId);

            Messenger.Default.Send(updateVM, "InvoiceUpdate");
        }

        private void ShowDelete(InvoiceViewModel invoice)
        {
            if (invoice == null) return;

            var deleteVM = new DeleteInvoiceViewModel();
            deleteVM.item = SharedData_Entities.Invoices
                .FirstOrDefault(i => i.InvoiceId == invoice.InvoiceId);

            Messenger.Default.Send(deleteVM, "InvoiceDelete");
        }
        */
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<InvoiceViewModel>
            (
                SharedData_Entities.Invoices
                    .Include(i => i.PaymentMethodNavigation)
                    .Where(i => i.IsActive == true)
                    .AsEnumerable()
                    .Select((i, index) =>
                    {
                        var vm = new InvoiceViewModel(i);
                        vm.Position = index + 1;
                        return vm;
                    })
            );
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string>
            {
                "InvoiceNumber",
                "InvoiceDate",
                "TotalGross",
                "IsPaid"
            };
        }

        public override void Sort()
        {
            switch (SortField)
            {
                case "InvoiceNumber":
                    List = new ObservableCollection<InvoiceViewModel>(List.OrderBy(i => i.InvoiceNumber));
                    break;
                case "InvoiceDate":
                    List = new ObservableCollection<InvoiceViewModel>(List.OrderBy(i => i.InvoiceDate));
                    break;
                case "TotalGross":
                    List = new ObservableCollection<InvoiceViewModel>(List.OrderBy(i => i.TotalGross));
                    break;
                case "IsPaid":
                    List = new ObservableCollection<InvoiceViewModel>(List.OrderBy(i => i.IsPaid));
                    break;
            }
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string>
            {
                "InvoiceNumber",
                "OrderId"
            };
        }

        public override void Find()
        {
            base.Find();

            if (FindField == "InvoiceNumber")
                List = new ObservableCollection<InvoiceViewModel>(
                    List.Where(i => i.InvoiceNumber != null &&
                        i.InvoiceNumber.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase)));

            if (FindField == "OrderId")
                List = new ObservableCollection<InvoiceViewModel>(
                    List.Where(i => i.OrderId.ToString().StartsWith(FindTextBox)));
        }
        #endregion
    }
}

