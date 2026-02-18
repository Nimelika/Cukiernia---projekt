using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllOrdersViewModel : AllViewModel<OrderViewModel>
    {
        #region Constructor
        public AllOrdersViewModel()
            : base("Orders")
        {
            DetailsCommand = new RelayCommand<OrderViewModel>(ShowDetails);
            UpdateCommand = new RelayCommand<OrderViewModel>(ShowUpdate);
            DeleteCommand = new RelayCommand<OrderViewModel>(ShowDelete);

            Messenger.Default.Register<string>(this, "Order", msg => { Load(); });
        }
        #endregion

        #region Properties
        private OrderViewModel _SelectedOrder;
        public OrderViewModel SelectedOrder
        {
            get => _SelectedOrder;
            set
            {
                if (value != _SelectedOrder)
                {
                    _SelectedOrder = value;
                    Messenger.Default.Send(_SelectedOrder);
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand<OrderViewModel> DetailsCommand { get; set; }
        public RelayCommand<OrderViewModel> UpdateCommand { get; set; }
        public RelayCommand<OrderViewModel> DeleteCommand { get; set; }

        private void ShowDetails(OrderViewModel order)
        {
            if (order == null) return;

            var displayVM = new DisplayOrderViewModel();
            displayVM.Load(order.OrderId);

            Messenger.Default.Send(displayVM, "OrderDisplay");
        }


        private void ShowUpdate(OrderViewModel order)
        {
            if (order == null) return;

            var updateVM = new UpdateOrderViewModel();
            updateVM.Load(order.OrderId);

            Messenger.Default.Send(updateVM, "OrderUpdate");
        }


        private void ShowDelete(OrderViewModel order)
        {
            if (order == null) return;

            var deleteVM = new DeleteOrderViewModel();
            deleteVM.item = SharedData_Entities.Orders
                .FirstOrDefault(o => o.OrderId == order.OrderId);

            Messenger.Default.Send(deleteVM, "OrderDelete");
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<OrderViewModel>
            (
                SharedData_Entities.Orders
                    .Include(o => o.CustomerNavigation)
                    .Include(o => o.ShopNavigation)
                    .Include(o => o.StatusNavigation)
                    .Where(o => o.IsActive == true)
                    .AsEnumerable()
                    .Select((o, index) =>
                    {
                        var vm = new OrderViewModel(o);
                        vm.Position = index + 1;
                        return vm;
                    })
            );
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string>
            {
                "OrderId",
                "OrderDate",
                "Customer",
                "Status"
            };
        }

        public override void Sort()
        {
            switch (SortField)
            {
                case "OrderId":
                    List = new ObservableCollection<OrderViewModel>(List.OrderBy(o => o.OrderId));
                    break;
                case "OrderDate":
                    List = new ObservableCollection<OrderViewModel>(List.OrderBy(o => o.OrderDate));
                    break;
                case "Customer":
                    List = new ObservableCollection<OrderViewModel>(List.OrderBy(o => o.Customer));
                    break;
                case "Status":
                    List = new ObservableCollection<OrderViewModel>(List.OrderBy(o => o.Status));
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
                List = new ObservableCollection<OrderViewModel>(
                    List.Where(o => o.Customer != null &&
                        o.Customer.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase)));

            if (FindField == "GuestEmail")
                List = new ObservableCollection<OrderViewModel>(
                    List.Where(o => o.GuestEmail != null &&
                        o.GuestEmail.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase)));

            if (FindField == "Status")
                List = new ObservableCollection<OrderViewModel>(
                    List.Where(o => o.Status != null &&
                        o.Status.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase)));
        }
        #endregion
    }
}
