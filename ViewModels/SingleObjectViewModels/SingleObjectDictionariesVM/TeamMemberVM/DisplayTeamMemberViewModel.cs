using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.TeamMemberVM
{
    public class DisplayTeamMemberViewModel
        : DisplayViewModel<TeamMember>
    {
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public DisplayTeamMemberViewModel()
            : base("Team Member Details")
        {
        }

        public void Load(int id)
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

        public string Name => item.Name;
        public string Description => item.Description;
        public string ImageAlt => item.ImageAlt;
        public bool? IsActive => item.IsActive;
        public string ImageUrl => item.ImageUrl;

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
