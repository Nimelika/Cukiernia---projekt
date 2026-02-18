using MakeAWishDB.Entities;
using System;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class OrderViewModel
    {
        public Order Item { get; }

        public OrderViewModel(Order item)
        {
            Item = item;
        }

        public int Position { get; set; }

        public int OrderId => Item.OrderId;

        public DateTime? OrderDate => Item.OrderDate;

        public string Customer =>
            Item.CustomerNavigation != null
                ? Item.CustomerNavigation.FullName
                : "Guest";

        public string GuestName => Item.GuestName;
        public string GuestEmail => Item.GuestEmail;
        public string GuestPhone => Item.GuestPhone;

        public string Shop =>
            Item.ShopNavigation != null
                ? Item.ShopNavigation.InternalName
                : string.Empty;

        public DateTime? CollectionDate => Item.CollectionDate;

        public string Status =>
            Item.StatusNavigation != null
                ? Item.StatusNavigation.Name
                : string.Empty;
    }
}

