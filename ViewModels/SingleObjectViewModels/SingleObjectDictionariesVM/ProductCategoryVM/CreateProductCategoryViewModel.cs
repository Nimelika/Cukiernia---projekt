using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.ProductCategoryVM
{
    public class CreateProductCategoryViewModel
        : CreateViewModel<ProductCategory>
    {
        public CreateProductCategoryViewModel()
            : base("Add Product Category")
        {
            UploadImageCommand = new RelayCommand(UploadImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
        }

        public string Name
        {
            get => item.Name;
            set
            {
                if (item.Name != value)
                {
                    item.Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string ImageAlt
        {
            get => item.ImageAlt;
            set
            {
                if (item.ImageAlt != value)
                {
                    item.ImageAlt = value;
                    OnPropertyChanged(() => ImageAlt);
                }
            }
        }

        public byte[] ImageData
        {
            get => item.ImageData;
            set
            {
                if (item.ImageData != value)
                {
                    item.ImageData = value;
                    OnPropertyChanged(() => ImageData);
                    OnPropertyChanged(() => ImagePreview);
                }
            }
        }

        public BitmapImage ImagePreview
        {
            get
            {
                if (ImageData == null || ImageData.Length == 0)
                    return null;

                try
                {
                    using (var ms = new MemoryStream(ImageData))
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
                catch
                {
                    return null;
                }
            }
        }

        public ICommand UploadImageCommand { get; }
        public ICommand DeleteImageCommand { get; }

        private void UploadImage()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                ImageData = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void DeleteImage()
        {
            ImageData = null;
        }

       
    }
}
