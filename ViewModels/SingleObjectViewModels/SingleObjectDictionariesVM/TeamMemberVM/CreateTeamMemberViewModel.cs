using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.TeamMemberVM
{
    public class CreateTeamMemberViewModel
        : CreateViewModel<TeamMember>
    {
        // WSPÓLNY KATALOG DLA DESKTOP + WEB
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public CreateTeamMemberViewModel()
            : base("Add Team Member")
        {
            UploadImageCommand = new RelayCommand(UploadImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
        }

        protected override void InitializeNewItem()
        {
            item.IsActive = true;
        }

        public string Name
        {
            get => item.Name;
            set
            {
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get => item.Description;
            set
            {
                item.Description = value;
                OnPropertyChanged(() => Description);
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
