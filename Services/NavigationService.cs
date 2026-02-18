using DesktopApp.ViewModels;

namespace DesktopApp.Services
{
    public class NavigationService : BaseViewModel
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(() => CurrentViewModel);
            }
        }

        public void NavigateTo(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}



