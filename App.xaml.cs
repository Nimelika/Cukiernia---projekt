using DesktopApp.Services;
using DesktopApp.ViewModels;
using DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels;
using MakeAWishDB.Context;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace DesktopApp
{
    public partial class App : Application
    {
        private SharedData_Entities _db;
        private CurrentUserService _currentUser;
        private AuthService _authService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // DbContext
            var optionsBuilder = new DbContextOptionsBuilder<SharedData_Entities>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=MakeAWishDBProd.Data;Trusted_Connection=True;MultipleActiveResultSets=true");

            _db = new SharedData_Entities(optionsBuilder.Options);

            // Services
            _currentUser = new CurrentUserService();
            _authService = new AuthService(_db);

            // Główny ViewModel aplikacji (Workspace + TabControl)
            var mainWindowViewModel = new MainWindowViewModel(_currentUser, _authService);



            // Login jako pierwsza zakładka
            var loginViewModel = new LoginViewModel(
                _authService,
                _currentUser);

            mainWindowViewModel.Workspaces.Add(loginViewModel);

            // Main Window
            var window = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            window.Show();
        }
    }
}
