using MakeAWishDB.Entities;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class MainPageArticleViewModel
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public MainPageArticle Item { get; }

        public MainPageArticleViewModel(MainPageArticle item)
        {
            Item = item;
        }

        // Pozycja na liście (np. numer wiersza)
        public int Position { get; set; }

        public int ArticleId => Item.ArticleId;
        public string Title => Item.Title;
        public string ImageAlt => Item.ImageAlt;
        public DateTime? PublishDate => Item.PublishDate;
        public bool? IsPublished => Item.IsPublished;
        public bool IsActive => Item.IsActive;

        // =========================
        // MINIATURA (DESKTOP APP)
        // =========================
        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Item.ImageUrl))
                    return null;

                try
                {
                    var fileName = Path.GetFileName(Item.ImageUrl);
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
