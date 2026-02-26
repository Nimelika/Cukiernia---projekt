using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.PageHeroImageVM
{
    public class CreatePageHeroImageViewModel
        : CreateViewModel<PageHeroImage>
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public CreatePageHeroImageViewModel()
            : base("Add Page Hero Image")
        {
            UploadImageCommand = new RelayCommand(UploadImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);

            LoadPageHeaders();
        }

        protected override void InitializeNewItem()
        {
            item.IsActive = true;
            item.IsVisible = true;
            item.CreatedAt = DateTime.Now;
            item.DisplayOrder = 1;
        }

        // =========================
        // PAGE HEADERS (COMBOBOX)
        // =========================
        public ObservableCollection<PageHeader> PageHeaders { get; }
            = new ObservableCollection<PageHeader>();

        private void LoadPageHeaders()
        {
            var headers = sharedData_Entities.PageHeaders
                .Where(p => p.IsActive == true && p.IsVisible == true)
                .OrderBy(p => p.PageHeaderId)
                .ToList();

            PageHeaders.Clear();
            foreach (var header in headers)
                PageHeaders.Add(header);
        }

        public int PageHeaderId
        {
            get => item.PageHeaderId;
            set
            {
                item.PageHeaderId = value;
                OnPropertyChanged(() => PageHeaderId);
            }
        }

        // =========================
        // FIELDS
        // =========================
        public string ImageAlt
        {
            get => item.ImageAlt;
            set
            {
                item.ImageAlt = value;
                OnPropertyChanged(() => ImageAlt);
            }
        }

        public int DisplayOrder
        {
            get => item.DisplayOrder;
            set
            {
                item.DisplayOrder = value;
                OnPropertyChanged(() => DisplayOrder);
            }
        }

        public bool? IsVisible
        {
            get => item.IsVisible;
            set
            {
                item.IsVisible = value;
                OnPropertyChanged(() => IsVisible);
            }
        }

        // =========================
        // IMAGE
        // =========================
        public string ImagePath
        {
            get => item.ImagePath;
            set
            {
                item.ImagePath = value;
                OnPropertyChanged(() => ImagePath);
                OnPropertyChanged(() => ImagePreview);
            }
        }

        public ICommand UploadImageCommand { get; }
        public ICommand DeleteImageCommand { get; }

        public BitmapImage ImagePreview
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImagePath))
                    return null;

                try
                {
                    var fileName = Path.GetFileName(ImagePath);
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

            ImagePath = "/uploads/" + fileName;
        }

        private void DeleteImage()
        {
            ImagePath = null;
        }
    }
}

