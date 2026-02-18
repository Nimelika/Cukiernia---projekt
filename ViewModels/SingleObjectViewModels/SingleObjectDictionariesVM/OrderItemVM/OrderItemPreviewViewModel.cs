using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderItemVM
{
    public class OrderItemPreviewViewModel
    {
        public int CelebrationCakeId { get; }
        public string CakeName { get; }
        public string CakeSizeName { get; }
        public decimal UnitPrice { get; }
        public int? Quantity { get; }
        public string PersonalizedText { get; }
        public string Notes { get; }

        public OrderItemPreviewViewModel(OrderItem item)
        {
            CelebrationCakeId = item.CelebrationCake;
            CakeName = item.CelebrationCakeNavigation?.Name ?? "—";
            CakeSizeName = item.CelebrationCakeSizeNavigation?.Name ?? "—";
            UnitPrice = item.UnitPrice;
            Quantity = item.Quantity;
            PersonalizedText = item.PersonalizedText;
            Notes = item.Notes;
        }
    }


}
