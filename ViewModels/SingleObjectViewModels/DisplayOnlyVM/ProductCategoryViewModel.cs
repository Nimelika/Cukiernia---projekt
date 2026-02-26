using MakeAWishDB.Entities;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class ProductCategoryViewModel
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

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

        // LADOWANIE MINIATURY 
        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Item.ImagePath))
                    return null;

                try
                {
                    var fileName = Path.GetFileName(Item.ImagePath);
                    var localPath = Path.Combine(SharedUploadsRoot, fileName);

                    if (!File.Exists(localPath))
                        return null;

                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(localPath, UriKind.Absolute);
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
