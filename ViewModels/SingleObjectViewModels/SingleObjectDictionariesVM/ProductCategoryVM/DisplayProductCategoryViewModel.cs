using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

    namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.ProductCategoryVM
    {
        public class DisplayProductCategoryViewModel : DisplayViewModel<ProductCategory>
        {
            public DisplayProductCategoryViewModel()
                : base("Display Product Category")
            {
            }

            public string CategoryName => item?.Name;

            public string ImageAlt => item?.ImageAlt;

            public BitmapImage ImagePreview
            {
                get
                {
                    if (item?.ImageData == null || item.ImageData.Length == 0)
                        return null;

                    using (var ms = new MemoryStream(item.ImageData))
                    {
                        var bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.CacheOption = BitmapCacheOption.OnLoad;
                        bmp.StreamSource = ms;
                        bmp.EndInit();
                        bmp.Freeze();
                        return bmp;
                    }
                }
            }
        }
    }

