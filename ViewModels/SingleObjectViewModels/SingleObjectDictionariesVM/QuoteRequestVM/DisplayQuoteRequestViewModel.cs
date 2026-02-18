using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.QuoteRequestVM
{
    public class DisplayQuoteRequestViewModel : DisplayViewModel<QuoteRequest>
    {
        public DisplayQuoteRequestViewModel()
            : base("Display Quote Request")
        {
        }

        public int QuoteId => item?.QuoteId ?? 0;

        public string Customer =>
            item?.CustomerNavigation != null
                ? item.CustomerNavigation.FullName
                : "Guest";

        public string GuestName => item?.GuestName;
        public string GuestEmail => item?.GuestEmail;
        public string Phone => item?.Phone;
        public string Description => item?.Description;

        public DateOnly? DesiredDeliveryDate => item?.DesiredDeliveryDate;

        public string Status =>
            item?.StatusNavigation != null
                ? item.StatusNavigation.Name
                : string.Empty;

        public bool? IsConvertedToOrder => item?.IsConvertedToOrder;

        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(item?.UploadedImagePath))
                    return null;

                try
                {
                    var sharedRoot = @"C:\MakeAWishShared";
                    var relativePath = item.UploadedImagePath.TrimStart('/');
                    var fullPath = Path.Combine(sharedRoot, relativePath);

                    if (!File.Exists(fullPath))
                        return null;

                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(fullPath, UriKind.Absolute);
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

