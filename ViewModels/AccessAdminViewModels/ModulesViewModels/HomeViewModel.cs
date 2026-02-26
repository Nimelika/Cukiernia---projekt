using DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Context;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.AccessAdminViewModels.ModulesViewModels
{
    public class HomeViewModel : WorkspaceViewModel
    {
        private readonly CurrentUserService _currentUser;
        private readonly SharedData_Entities _context;

        public HomeViewModel(CurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _context = new SharedData_Entities();

            DisplayName = "Home";

            LoadPermissions();
            LoadDashboardData();
        }

        
        // WELCOME
        
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

        
        // DASHBOARD COUNTERS
      
        public int NewOrdersCount { get; private set; }
        public int NewQuoteRequestsCount { get; private set; }

        private void LoadDashboardData()
        {
            NewOrdersCount = _context.ViewNewOrders.Count();
            NewQuoteRequestsCount = _context.ViewNewQuoteRequests.Count();

            OnPropertyChanged(() => NewOrdersCount);
            OnPropertyChanged(() => NewQuoteRequestsCount);
        }

        
        // MODULE PERMISSIONS
        
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

