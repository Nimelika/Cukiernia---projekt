using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.MainPageArticleVM
{
    public class DisplayMainPageArticleViewModel
        : DisplayViewModel<MainPageArticle>
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public DisplayMainPageArticleViewModel()
            : base("Main Page Article Details")
        {
        }

        public void Load(int id)
        {
            item = sharedData_Entities.MainPageArticles
                .FirstOrDefault(a => a.ArticleId == id);

            if (item == null)
                throw new InvalidOperationException("Main page article not found.");

            OnPropertyChanged(() => Title);
            OnPropertyChanged(() => Body);
            OnPropertyChanged(() => PublishDate);
            OnPropertyChanged(() => IsPublished);
            OnPropertyChanged(() => ImageAlt);
            OnPropertyChanged(() => ImageUrl);
        }

        public string Title => item.Title;
        public string Body => item.Body;
        public DateTime? PublishDate => item.PublishDate;
        public bool? IsPublished => item.IsPublished;
        public string ImageAlt => item.ImageAlt;
        public string ImageUrl => item.ImageUrl;

        // =========================
        // IMAGE PREVIEW
        // =========================
        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImageUrl))
                    return null;

                try
                {
                    var fileName = Path.GetFileName(ImageUrl);
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
