using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.QuoteRequestVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllQuoteRequestsViewModel : AllViewModel<QuoteRequestViewModel>
    {
        #region Constructor
        public AllQuoteRequestsViewModel()
            : base("Quote Requests")
        {
            DetailsCommand = new RelayCommand<QuoteRequestViewModel>(ShowDetails);
            UpdateCommand = new RelayCommand<QuoteRequestViewModel>(ShowUpdate);
            DeleteCommand = new RelayCommand<QuoteRequestViewModel>(ShowDelete);

            Messenger.Default.Register<string>(this, "QuoteRequest", msg => { Load(); });
        }
        #endregion

        #region Properties
        private QuoteRequestViewModel _SelectedQuote;
        public QuoteRequestViewModel SelectedQuote
        {
            get => _SelectedQuote;
            set
            {
                if (value != _SelectedQuote)
                {
                    _SelectedQuote = value;
                    Messenger.Default.Send(_SelectedQuote);
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand<QuoteRequestViewModel> DetailsCommand { get; set; }
        public RelayCommand<QuoteRequestViewModel> UpdateCommand { get; set; }
        public RelayCommand<QuoteRequestViewModel> DeleteCommand { get; set; }

        private void ShowDetails(QuoteRequestViewModel quote)
        {
            if (quote == null) return;

            var entity = SharedData_Entities.QuoteRequests
                .Include(q => q.CustomerNavigation)
                .Include(q => q.StatusNavigation)
                .FirstOrDefault(q => q.QuoteId == quote.QuoteId);

            if (entity != null)
            {
                var displayVM = new DisplayQuoteRequestViewModel();
                displayVM.item = entity;
                Messenger.Default.Send(displayVM, "QuoteRequestDisplay");
            }
        }

        private void ShowUpdate(QuoteRequestViewModel quote)
        {
            if (quote == null) return;

            var updateVM = new UpdateQuoteRequestViewModel();
            updateVM.Load(quote.QuoteId);
            Messenger.Default.Send(updateVM, "QuoteRequestUpdate");
        }

        private void ShowDelete(QuoteRequestViewModel quote)
        {
            if (quote == null) return;

            var entity = SharedData_Entities.QuoteRequests
                .Include(q => q.CustomerNavigation)
                .Include(q => q.StatusNavigation)
                .FirstOrDefault(q => q.QuoteId == quote.QuoteId);

            if (entity != null)
            {
                var deleteVM = new DeleteQuoteRequestViewModel();
                deleteVM.item = entity;
                Messenger.Default.Send(deleteVM, "QuoteRequestDelete");
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<QuoteRequestViewModel>
            (
                SharedData_Entities.QuoteRequests
                    .Include(q => q.CustomerNavigation)
                    .Include(q => q.StatusNavigation)
                    .Where(q => q.IsActive == true)
                    .AsEnumerable()
                    .Select((q, index) =>
                    {
                        var vm = new QuoteRequestViewModel(q);
                        vm.Position = index + 1;
                        return vm;
                    })
            );
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string>
            {
                "QuoteId",
                "Customer",
                "Status",
                "DesiredDeliveryDate"
            };
        }

        public override void Sort()
        {
            switch (SortField)
            {
                case "QuoteId":
                    List = new ObservableCollection<QuoteRequestViewModel>(List.OrderBy(q => q.QuoteId));
                    break;

                case "Customer":
                    List = new ObservableCollection<QuoteRequestViewModel>(List.OrderBy(q => q.Customer));
                    break;

                case "Status":
                    List = new ObservableCollection<QuoteRequestViewModel>(List.OrderBy(q => q.Status));
                    break;

                case "DesiredDeliveryDate":
                    List = new ObservableCollection<QuoteRequestViewModel>(List.OrderBy(q => q.DesiredDeliveryDate));
                    break;
            }
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string>
            {
                "Customer",
                "GuestEmail",
                "Status"
            };
        }

        public override void Find()
        {
            base.Find();

            if (FindField == "Customer")
                List = new ObservableCollection<QuoteRequestViewModel>
                (
                    List.Where(q =>
                        q.Customer != null &&
                        q.Customer.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase))
                );

            if (FindField == "GuestEmail")
                List = new ObservableCollection<QuoteRequestViewModel>
                (
                    List.Where(q =>
                        q.GuestEmail != null &&
                        q.GuestEmail.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase))
                );

            if (FindField == "Status")
                List = new ObservableCollection<QuoteRequestViewModel>
                (
                    List.Where(q =>
                        q.Status != null &&
                        q.Status.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase))
                );
        }
        #endregion
    }
}
