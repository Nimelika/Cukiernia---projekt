using DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels;
using DesktopApp.Services;

namespace DesktopApp.ViewModels.AccessAdminViewModels.MainViewModels
{
    public class MainViewModel
    {
        public MenuViewModel Menu { get; }
        public NavigationService Navigation { get; }

        public MainViewModel(
            CurrentUserService currentUser,
            NavigationService navigation)
        {
            Navigation = navigation;
            Menu = new MenuViewModel(currentUser, navigation);
        }
    }
}

