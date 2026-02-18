using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderVM
{
    public class DisplayOrderViewModel : DisplayViewModel<Order>
    {
        public DisplayOrderViewModel()
            : base("Order Details")
        {
        }

        public void Load(int id)
        {
            item = sharedData_Entities.Orders
                .Include(o => o.CustomerNavigation)
                .Include(o => o.ShopNavigation)
                .Include(o => o.StatusNavigation)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CelebrationCakeNavigation)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CelebrationCakeSizeNavigation)
                .FirstOrDefault(o => o.OrderId == id);

            LoadOrderItems();
        }

        public int OrderId => item.OrderId;
        public string Customer => item.CustomerNavigation?.FullName ?? "Guest";
        public string GuestName => item.GuestName;
        public string GuestEmail => item.GuestEmail;
        public string GuestPhone => item.GuestPhone;
        public string Shop => item.ShopNavigation?.InternalName;
        public string Status => item.StatusNavigation?.Name;
        public bool? IsPaid => item.IsPaid;
        public System.DateTime? OrderDate => item.OrderDate;
        public System.DateTime? CollectionDate => item.CollectionDate;

        public ObservableCollection<OrderItemPreviewViewModel> OrderItems { get; }
            = new();

        private void LoadOrderItems()
        {
            OrderItems.Clear();
            foreach (var oi in item.OrderItems)
                OrderItems.Add(new OrderItemPreviewViewModel(oi));
        }
    }
}

