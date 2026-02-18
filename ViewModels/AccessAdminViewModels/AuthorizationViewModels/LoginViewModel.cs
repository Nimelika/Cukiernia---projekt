using DesktopApp.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels
{
    public class LoginViewModel : WorkspaceViewModel
    {
        private readonly AuthService _authService;
        private readonly CurrentUserService _currentUser;

        private RelayCommand _loginCommand;
        public ICommand LoginCommand => _loginCommand;

        public LoginViewModel(
            AuthService authService,
            CurrentUserService currentUser)
        {
            _authService = authService;
            _currentUser = currentUser;

            DisplayName = "Login";

            _loginCommand = new RelayCommand(Login, CanLogin);
        }

        // Properties

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(() => Email);
                _loginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(() => Password);
                _loginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(() => ErrorMessage);
            }
        }

        // Command logic

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Password);
        }

        private void Login()
        {
            var user = _authService.Authenticate(Email, Password);

            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";
                return;
            }

            // Save user in session
            _currentUser.UserId = user.UserId;
            _currentUser.Email = user.Email;
            _currentUser.DisplayName = user.DisplayName;
            _currentUser.RoleName = user.RoleName;
            _currentUser.AllowedModuleCodes = user.AllowedModuleCodes;

            // Informacja o poprawnym logowaniu
            Messenger.Default.Send("LoginSuccess");
        }
    }
}
