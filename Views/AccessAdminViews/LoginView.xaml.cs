using DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels;
using System.Windows.Controls;

namespace DesktopApp.Views.AccessAdminViews
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}