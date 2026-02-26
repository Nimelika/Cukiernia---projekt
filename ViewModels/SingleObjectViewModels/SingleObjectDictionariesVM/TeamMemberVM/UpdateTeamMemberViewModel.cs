using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.TeamMemberVM
{
    public class UpdateTeamMemberViewModel
        : UpdateViewModel<TeamMember>
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public UpdateTeamMemberViewModel()
            : base("Edit Team Member")
        {
            UploadImageCommand = new RelayCommand(UploadImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
        }

        // =========================
        // LOAD EXISTING ENTITY
        // =========================
        public override void Load(int id)
        {
            item = sharedData_Entities.TeamMembers
                .FirstOrDefault(t => t.TeamMemberId == id);

            if (item == null)
                throw new InvalidOperationException("Team member not found.");

            OnPropertyChanged(() => Name);
            OnPropertyChanged(() => Description);
            OnPropertyChanged(() => ImageAlt);
            OnPropertyChanged(() => ImageUrl);
            OnPropertyChanged(() => IsActive);

        }

        // =========================
        // FIELDS
        // =========================
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

        public bool? IsActive
        {
            get => item.IsActive;
            set
            {
                item.IsActive = value;
                OnPropertyChanged(() => IsActive);
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
                case nameof(Name):
                    return string.IsNullOrWhiteSpace(Name)
                        ? "Name is required."
                        : null;
            }

            return null;
        }
    }
}
