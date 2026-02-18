using DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopApp.Services;
using DesktopApp.ViewModels.AccessAdminViewModels.ModulesViewModels;

namespace DesktopApp.ViewModels.AccessAdminViewModels.MainViewModels
{

    public class MenuViewModel
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        public MenuViewModel(
    CurrentUserService currentUser,
    NavigationService navigation)
        {
            _currentUser = currentUser;

            MenuItems = new ObservableCollection<MenuItemViewModel>();

            AddIfAllowed(
                "Home",
                "HOME",
                new RelayCommand(() => navigation.NavigateTo(new HomeViewModel(_currentUser)))
            );

            AddIfAllowed(
                "Products",
                "PRODUCTS",
                new RelayCommand(() => navigation.NavigateTo(new ProductsViewModel()))
            );

            AddIfAllowed(
                "Stock & Orders",
                "PRODUCTION",
                new RelayCommand(() => navigation.NavigateTo(new ProductionViewModel()))
            );

            AddIfAllowed(
                "Settings",
                "SETTINGS",
                new RelayCommand(() => navigation.NavigateTo(new SettingsViewModel()))
            );
        }

        private readonly CurrentUserService _currentUser;

        private void AddIfAllowed(string header, string code, ICommand command)
        {
            if (_currentUser?.AllowedModuleCodes?.Contains(code) == true)

            {
                MenuItems.Add(new MenuItemViewModel(header, code, command));
            }
        }

    }


}
