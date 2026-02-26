using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.CelebrationCakeVM
{
    public class DisplayCelebrationCakeViewModel
        : DisplayViewModel<CelebrationCake>
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public DisplayCelebrationCakeViewModel()
            : base("Celebration Cake Details")
        {
        }

        public void Load(int id)
        {
            item = sharedData_Entities.CelebrationCakes
                .First(c => c.CelebrationCakeId == id);

            OnPropertyChanged(() => Name);
            OnPropertyChanged(() => Description);
            OnPropertyChanged(() => ImageAlt);
            OnPropertyChanged(() => PriceSmall);
            OnPropertyChanged(() => PriceMedium);
            OnPropertyChanged(() => PriceLarge);
            OnPropertyChanged(() => ImagePreview);
        }

        public string Name => item.Name;
        public string Description => item.Description;
        public string ImageAlt => item.ImageAlt;

        public decimal? PriceSmall => item.PriceSmall;
        public decimal? PriceMedium => item.PriceMedium;
        public decimal? PriceLarge => item.PriceLarge;

        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(item.ImageUrl))
                    return null;

                try
                {
                    var fileName = Path.GetFileName(item.ImageUrl);
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

