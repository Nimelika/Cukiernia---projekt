using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderVM
{
    public class DeleteOrderViewModel : DeleteViewModel<Order>
    {
        public DeleteOrderViewModel()
            : base("Delete Order")
        {
        }

        protected override string RefreshMessageTag => "Orders";

        public int OrderId => item.OrderId;
        public string Customer => item.CustomerNavigation?.FullName ?? "Guest";
    }
}

