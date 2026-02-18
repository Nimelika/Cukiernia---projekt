using MakeAWishDB.Entities;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class QuoteRequestViewModel
    {
        public QuoteRequest Item { get; }

        public QuoteRequestViewModel(QuoteRequest item)
        {
            Item = item;
        }

        public int Position { get; set; }

        public int QuoteId => Item.QuoteId;

        public string Customer =>
            Item.CustomerNavigation != null
                ? Item.CustomerNavigation.FullName
                : "Guest";

        public string GuestName => Item.GuestName;
        public string GuestEmail => Item.GuestEmail;
        public string Phone => Item.Phone;
        public string Description => Item.Description;

        public DateOnly? DesiredDeliveryDate => Item.DesiredDeliveryDate;

        public string Status =>
            Item.StatusNavigation != null
                ? Item.StatusNavigation.Name
                : string.Empty;

        public bool? IsConvertedToOrder => Item.IsConvertedToOrder;

        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Item.UploadedImagePath))
                    return null;

                try
                {
                    var sharedRoot = @"C:\MakeAWishShared";
                    var relativePath = Item.UploadedImagePath.TrimStart('/');
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

