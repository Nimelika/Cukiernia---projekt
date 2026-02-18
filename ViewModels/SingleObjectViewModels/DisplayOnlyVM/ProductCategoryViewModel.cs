using MakeAWishDB.Entities;
using System.IO;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class ProductCategoryViewModel
    {
        public ProductCategory Item { get; }

        public ProductCategoryViewModel(ProductCategory item)
        {
            Item = item;
        }

        public int Position { get; set; }

        public int ProductCategoryId => Item.ProductCategoryId;

        public string Name => Item.Name;

        public string ImageAlt => Item.ImageAlt;

        public bool? IsActive => Item.IsActive;

        public BitmapImage ImagePreview
        {
            get
            {
                if (Item.ImageData == null || Item.ImageData.Length == 0)
                    return null;

                try
                {
                    using (var memoryStream = new MemoryStream(Item.ImageData))
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();
                        return bitmapImage;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
