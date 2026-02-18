using MakeAWishDB.Entities;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class OrderItemPreviewViewModel
    {
        private readonly OrderItem _item;

        public OrderItemPreviewViewModel(OrderItem item)
        {
            _item = item;
        }

        public int OrderItemId => _item.OrderItemId;

        public string CelebrationCake =>
            _item.CelebrationCakeNavigation?.Name ?? string.Empty;

        public int? Quantity => _item.Quantity;

        public string CelebrationCakeSize =>
            _item.CelebrationCakeSizeNavigation?.Name ?? string.Empty;

        public string PersonalizedText => _item.PersonalizedText;
        public string Notes => _item.Notes;
    }
}
