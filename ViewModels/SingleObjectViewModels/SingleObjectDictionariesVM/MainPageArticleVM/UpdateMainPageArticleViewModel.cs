using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.MainPageArticleVM
{
    public class UpdateMainPageArticleViewModel
        : UpdateViewModel<MainPageArticle>
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public UpdateMainPageArticleViewModel()
            : base("Edit Main Page Article")
        {
            UploadImageCommand = new RelayCommand(UploadImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
        }

        // =========================
        // LOAD EXISTING ARTICLE
        // =========================
        public override void Load(int id)
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

        // =========================
        // FIELDS
        // =========================
        public string Title
        {
            get => item.Title;
            set
            {
                item.Title = value;
                OnPropertyChanged(() => Title);
            }
        }

        public string Body
        {
            get => item.Body;
            set
            {
                item.Body = value;
                OnPropertyChanged(() => Body);
            }
        }

        public DateTime? PublishDate
        {
            get => item.PublishDate;
            set
            {
                item.PublishDate = value;
                OnPropertyChanged(() => PublishDate);
            }
        }

        public bool? IsPublished
        {
            get => item.IsPublished;
            set
            {
                item.IsPublished = value;
                OnPropertyChanged(() => IsPublished);
            }
        }

        public string ImageAlt
        {
            get => item.ImageAlt;
            set
            {
                item.ImageAlt = value;
                OnPropertyChanged(() => ImageAlt);
            }
        }

        // =========================
        // IMAGE
        // =========================
        public string ImageUrl
        {
            get => item.ImageUrl;
            set
            {
                item.ImageUrl = value;
                OnPropertyChanged(() => ImageUrl);
                OnPropertyChanged(() => ImagePreview);
            }
        }

        public ICommand UploadImageCommand { get; }
        public ICommand DeleteImageCommand { get; }

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

        private void UploadImage()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() != true)
                return;

            Directory.CreateDirectory(SharedUploadsRoot);

            var fileName = Guid.NewGuid() + Path.GetExtension(dialog.FileName);
            var fullPath = Path.Combine(SharedUploadsRoot, fileName);

            File.Copy(dialog.FileName, fullPath, overwrite: true);

            ImageUrl = "/uploads/" + fileName;
        }

        private void DeleteImage()
        {
            ImageUrl = null;
        }

        // =========================
        // VALIDATION
        // =========================
        protected override string ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Title):
                    return string.IsNullOrWhiteSpace(Title)
                        ? "Title is required."
                        : null;

                case nameof(Body):
                    return string.IsNullOrWhiteSpace(Body)
                        ? "Article body is required."
                        : null;
            }

            return null;
        }
    }
}

