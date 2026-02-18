using MakeAWishDB.Entities;
using System.IO;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class CelebrationCakeViewModel
    {
        public CelebrationCake Item { get; }

        public CelebrationCakeViewModel(CelebrationCake item)
        {
            Item = item;
        }

        public int Position { get; set; }

        public int CelebrationCakeId => Item.CelebrationCakeId;
        public string Name => Item.Name;
        public string Description => Item.Description;
        public string ImageAlt => Item.ImageAlt;
        public string CakeFilling => Item.CakeFillingNavigation?.Name;

        public decimal? PriceSmall => Item.PriceSmall;
        public decimal? PriceMedium => Item.PriceMedium;
        public decimal? PriceLarge => Item.PriceLarge;

        public bool? WithNuts => Item.WithNuts;
        public bool? WithFruits => Item.WithFruits;
        public bool? WithAlcohol => Item.WithAlcohol;
        public bool? Vegan => Item.Vegan;
        public bool? LowSugar => Item.LowSugar;
        public bool? NoSugar => Item.NoSugar;
        public bool? WeddingOffer => Item.WeddingOffer;
        public bool? ChildrenOffer => Item.ChildrenOffer;

        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Item.ImageUrl))
                    return null;

                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new System.Uri(Item.ImageUrl, System.UriKind.Absolute);
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return bitmap;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}

