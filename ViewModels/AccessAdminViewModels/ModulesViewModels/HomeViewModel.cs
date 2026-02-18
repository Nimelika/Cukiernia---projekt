using DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.AccessAdminViewModels.ModulesViewModels

    {
    public class HomeViewModel : WorkspaceViewModel
    {
        private readonly CurrentUserService _currentUser;

        public HomeViewModel(CurrentUserService currentUser)
        {
            _currentUser = currentUser;
            DisplayName = "Home";

            LoadPermissions();
        }

        public string WelcomeText
        {
            get
            {
                var name = _currentUser.DisplayName;

                if (string.IsNullOrWhiteSpace(name))
                    return "Welcome";

                name = name.Trim();

                return name.Length == 1
                    ? $"Welcome {name.ToUpper()}"
                    : $"Welcome {char.ToUpper(name[0])}{name.Substring(1)}";
            }
        }


        //kolekcja uprawnien do modulow, ktora bedzie wyswietlana w UI
        public ObservableCollection<ModulePermissionViewModel> Permissions { get; }
    = new ObservableCollection<ModulePermissionViewModel>();

        private void LoadPermissions()
        {
            var allModules = new[]
            {
        ("HOME", "Home"),
        ("PRODUCTS", "Products"),
        ("PRODUCTION", "Production"),
        ("FINANCE", "Finance"),
        ("ADMIN", "Administration"),
        ("SETTINGS", "Settings"),
        ("WEBPAGE", "Webpage")
    };

            Permissions.Clear();

            foreach (var (code, name) in allModules)
            {
                Permissions.Add(new ModulePermissionViewModel(
                    code,
                    name,
                    _currentUser.AllowedModuleCodes.Contains(code)
                ));
            }
        }

    }
}

