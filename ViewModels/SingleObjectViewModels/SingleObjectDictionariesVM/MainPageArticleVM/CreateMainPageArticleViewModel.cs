using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.MainPageArticleVM
{
    public class CreateMainPageArticleViewModel
        : CreateViewModel<MainPageArticle>
    {
        // WSPÓLNY KATALOG DLA DESKTOP + WEB
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public CreateMainPageArticleViewModel()
            : base("Add Main Page Article")
        {
            UploadImageCommand = new RelayCommand(UploadImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
        }

        protected override void InitializeNewItem()
        {
            item.IsActive = true;
            item.IsPublished = false;
            item.PublishDate = DateTime.Now;
        }

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

        // ŚCIEŻKA WEBOWA (DB + WEB)
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

        // MINIATURA W DESKTOPAPP
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

            // zapis ścieżki WEBOWEJ do DB
            ImageUrl = "/uploads/" + fileName;
        }

        private void DeleteImage()
        {
            ImageUrl = null;
        }
    }
}
