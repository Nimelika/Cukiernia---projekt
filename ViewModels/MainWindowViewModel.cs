using DesktopApp.Helpers;
using DesktopApp.Services;
using DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels;
using DesktopApp.ViewModels.AccessAdminViewModels.ModulesViewModels;
using DesktopApp.ViewModels.AccessAdminViewModels.RoleAccessViewModels;
using DesktopApp.ViewModels.MulitipleObjectsViewModels;
using DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM;
using DesktopApp.ViewModels.SingleObjectViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.CelebrationCakeVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.InvoiceItemVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.InvoiceVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.MainPageArticleVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderItemVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.PageHeroImageVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.ProductCategoryVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.QuoteRequestVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.RegionVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.TeamMemberVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.UserAccountVM;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopApp.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Kontruktor
        public MainWindowViewModel(CurrentUserService currentUser, AuthService authService)
        {
            _authService = authService;
            _currentUser = currentUser; 
            LoginViewModel = new LoginViewModel(authService, currentUser);
            
            Messenger.Default.Register<string>(this, msg => {
            if (msg == "LoginSuccess")
            {
                Workspaces.Clear(); 
                    Workspaces.Add(new HomeViewModel(_currentUser));
                    OnPropertyChanged(() => IsUserLoggedIn);
                    // odœwie¿enie komend po loginie
                    CommandManager.InvalidateRequerySuggested(); } });
                Messenger.Default.Register<DeleteRegionViewModel>(this, "RegionDelete", OnRegionDeleteMessageReceived);
                Messenger.Default.Register<DeleteUserAccountViewModel>(this, "UserAccessDelete", OnUserAccountDeleteMessageReceived);
                Messenger.Default.Register<DeleteCelebrationCakeViewModel>(this, "CelebrationCakeDelete", OnCelebrationCakeDeleteMessageReceived);
            Messenger.Default.Register<DeleteProductCategoryViewModel>(this, "ProductCategoryDelete", OnProductCategoryDeleteMessageReceived);
            Messenger.Default.Register<DeleteQuoteRequestViewModel>(this, "QuoteRequestDelete", OnQuoteRequestDeleteMessageReceived);
            Messenger.Default.Register<DeleteOrderViewModel>(this, "OrderDelete", OnOrderDeleteMessageReceived);
            Messenger.Default.Register<DeleteMainPageArticleViewModel>(this, "MainPageArticleDelete", OnMainPageArticleDeleteMessageReceived);
            Messenger.Default.Register<DeleteTeamMemberViewModel>(this, "TeamMemberDelete", OnTeamMemberDeleteMessageReceived);
            Messenger.Default.Register<UpdateRegionViewModel>(this, "RegionUpdate", OnRegionUpdateMessageReceived);
            Messenger.Default.Register<UpdateUserAccountViewModel>(this, "UserAccessUpdate", OnUserAccountUpdateMessageReceived);
            Messenger.Default.Register<UpdateCelebrationCakeViewModel>(this, "CelebrationCakeUpdate", OnCelebrationCakeUpdateMessageReceived);
            Messenger.Default.Register<UpdateProductCategoryViewModel>(this, "ProductCategoryUpdate", OnProductCategoryUpdateMessageReceived);
            Messenger.Default.Register<UpdateOrderViewModel>(this, "OrderUpdate", OnOrderUpdateMessageReceived);
            Messenger.Default.Register<UpdateMainPageArticleViewModel>(this, "MainPageArticleUpdate", OnMainPageArticleUpdateMessageReceived);
            Messenger.Default.Register<UpdateTeamMemberViewModel>(this, "TeamMemberUpdate", OnTeamMemberUpdateMessageReceived);
            Messenger.Default.Register<UpdateQuoteRequestViewModel>(this, "QuoteRequestUpdate", OnQuoteRequestUpdateMessageReceived);
            Messenger.Default.Register<DisplayRegionViewModel>(this, "RegionDisplay", OnRegionDisplayMessageReceived);
            Messenger.Default.Register<DisplayUserAccountViewModel>(this, "UserAccessDisplay", OnUserAccountDisplayMessageReceived);
            Messenger.Default.Register<DisplayCelebrationCakeViewModel>(this, "CelebrationCakeDisplay", OnCelebrationCakeDisplayMessageReceived);
            Messenger.Default.Register<DisplayProductCategoryViewModel>(this, "ProductCategoryDisplay", OnProductCategoryDisplayMessageReceived);
            Messenger.Default.Register<DisplayQuoteRequestViewModel>(this, "QuoteRequestDisplay", OnQuoteRequestDisplayMessageReceived);
            Messenger.Default.Register<DisplayOrderViewModel>(this, "OrderDisplay", OnOrderDisplayMessageReceived);
            Messenger.Default.Register<DisplayMainPageArticleViewModel>(this, "MainPageArticleDisplay", OnMainPageArticleDisplayMessageReceived);
            Messenger.Default.Register<DisplayTeamMemberViewModel>(this, "TeamMemberDisplay", OnTeamMemberDisplayMessageReceived);

            Messenger.Default.Register<string>(this, msg => { if (msg == "Add Regions") { CreateRegionCommand.Execute(null); } });
            Messenger.Default.Register<string>(this, msg => { if (msg == "Add Celebration Cake") { CreateCelebrationCakeCommand.Execute(null); } });
            Messenger.Default.Register<string>(this, msg => { if (msg == "Add Main Page Article") { CreateMainPageArticleCommand.Execute(null); } });
            Messenger.Default.Register<string>(this, msg => { if (msg == "Add Team Member") { CreateTeamMemberCommand.Execute(null); } });
            Messenger.Default.Register<string>(this, msg => { if (msg == "Add Product Categories") { CreateProductCategoryCommand.Execute(null); } });
            Messenger.Default.Register<string>(this, msg => { if (msg == "Add Orders") { CreateOrderCommand.Execute(null); } });
            Messenger.Default.Register<string>(this, msg => { if (msg == "Add User Access") { CreateUserAccountCommand.Execute(null); } });
            Messenger.Default.Register<string>(this, msg =>
            {
                if (msg == "LoginSuccess")
                {
                    Workspaces.Clear();
                    Workspaces.Add(new HomeViewModel(_currentUser));
                    OnPropertyChanged(() => IsUserLoggedIn);
                    CommandManager.InvalidateRequerySuggested();
                }

            });
            Messenger.Default.Register<ManageRoleModulesViewModel>(
    this, "ManageRoleModules",
    vm => createView(vm));


        }
        private readonly CurrentUserService _currentUser;
        public bool IsUserLoggedIn => _currentUser.IsLoggedIn;
        public LoginViewModel LoginViewModel { get; }
        private readonly AuthService _authService;

        #endregion


        #region Komendy menu i paska narzedzi

        public ICommand LogoutCommand =>
    new BaseCommand(Logout);


        public ICommand AllProductCategoriesCommand =>
    new BaseCommand(
        showAllProductCategories,
        () => _currentUser.AllowedModuleCodes.Contains("PRODUCTS")
    );
        public ICommand AllCelebrationCakesCommand =>
    new BaseCommand(
        showAllCelebrationCakes,
        () => _currentUser.AllowedModuleCodes.Contains("PRODUCTS")
    );
        public ICommand CreateCelebrationCakeCommand =>
           new BaseCommand(
               () => createView(new CreateCelebrationCakeViewModel()),
               () => _currentUser.AllowedModuleCodes.Contains("PRODUCTS")
           );


        public ICommand CreateProductCategoryCommand =>
            new BaseCommand(
                () => createView(new CreateProductCategoryViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("PRODUCTS")
            );
        public ICommand AllQuoteRequestsCommand =>
           new BaseCommand(
               showAllQuoteRequests,
               () => _currentUser.AllowedModuleCodes.Contains("PRODUCTION")
           );
        public ICommand AllOrdersCommand =>
           new BaseCommand(
               showAllOrders,
               () => _currentUser.AllowedModuleCodes.Contains("PRODUCTION")
           );

        public ICommand CreateOrderCommand =>
    new BaseCommand(
        () => createView(new CreateOrderViewModel()),
        () => _currentUser.AllowedModuleCodes.Contains("PRODUCTION")
    );

        public ICommand CreateOrderItemCommand =>
            new BaseCommand(
                () => createView(new CreateOrderItemViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("PRODUCTION")
            );

        public ICommand CreateInvoiceCommand =>
    new BaseCommand(
        () => createView(new CreateInvoiceViewModel()),
        () => _currentUser.AllowedModuleCodes.Contains("FINANCE")
    );

        public ICommand CreateInvoiceItemCommand =>
            new BaseCommand(
                () => createView(new CreateInvoiceItemViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("FINANCE")
            );
        public ICommand AllInvoicesCommand =>
            new BaseCommand(
                showAllInvoices,
                () => _currentUser.AllowedModuleCodes.Contains("FINANCE")
            );

        public ICommand AllRegionsCommand =>
    new BaseCommand(
        showAllRegions,
        () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
    );
        public ICommand AllMainPageArticlesCommand =>
    new BaseCommand(
        showAllMainPageArticles,
        () => _currentUser.AllowedModuleCodes.Contains("WEBPAGE")
    );
        public ICommand AllUserAccessesCommand =>
    new BaseCommand(
        showAllUserAccesses,
        () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
    );
        public ICommand CreateUserAccountCommand =>
new BaseCommand(
   () => createView(new CreateUserAccountViewModel()),
   () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
);
        public ICommand ManageRoleModulesCommand =>
  new BaseCommand(
     () => createView(new ManageRoleModulesViewModel()),
     () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
  );

        public ICommand CreateRegionCommand =>
            new BaseCommand(
                () => createView(new CreateRegionViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
            );
       

        public ICommand CreateTeamMemberCommand =>
            new BaseCommand(
                () => createView(new CreateTeamMemberViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("WEBPAGE")
            );
        public ICommand CreateMainPageArticleCommand =>
            new BaseCommand(
                () => createView(new CreateMainPageArticleViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("WEBPAGE")
            );
        public ICommand CreatePageHeroImageCommand =>
            new BaseCommand(
                () => createView(new CreatePageHeroImageViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("WEBPAGE")
            );

        public ICommand AllCountriesCommand =>
            new BaseCommand(
                showAllCountries,
                () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
            );

        public ICommand CreateCountryCommand =>
            new BaseCommand(
                () => createView(new CreateCountryViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
            );

        public ICommand AllModulesCommand =>
            new BaseCommand(
                showAllModules,
                () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
            );

        public ICommand CreateModuleCommand =>
            new BaseCommand(
                () => createView(new CreateModuleViewModel()),
                () => _currentUser.AllowedModuleCodes.Contains("ADMIN")
            );
        public ICommand AllTeamMembersCommand =>
            new BaseCommand(
                showAllTeamMembers,
                () => _currentUser.AllowedModuleCodes.Contains("WEBPAGE")
            );




        public ICommand CreateCakeFillingCommand
        {
			get
			{
				return new BaseCommand(() => createView(new CreateCakeFillingViewModel()));
			}
		}
		public ICommand CreateCelebrationCakeSizeCommand
        {
			get
			{
				return new BaseCommand(() => createView(new CreateCelebrationCakeSizeViewModel()));
			}
		}
		
		
		
		public ICommand CreatePaymentMethodCommand
        {
			get
			{
				return new BaseCommand(() => createView(new CreatePaymentMethodViewModel()));
			}
		}

		

       

       

		public ICommand AllCakeFillingsCommand
		{
			get
			{
				return new BaseCommand(showAllCakeFillings);
			}
		}

		public ICommand AllCelebrationCakeSizesCommand
        {
			get
			{
				return new BaseCommand(showAllCelebrationCakeSizes);
			}
		}

		public ICommand AllPaymentMethodsCommand
        {
			get
			{
				return new BaseCommand(showAllPaymentMethods);
			}
		}

		

		

		

		        #endregion


        #region Przyciski w menu z lewej strony
        private ReadOnlyCollection<CommandViewModel> _Commands;//to jest kolekcja komend w emnu lewym
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)//sprawdzam czy przyciski z lewej strony menu nie zosta³y zainicjalizowane
                {
                    List<CommandViewModel> cmds = this.CreateCommands();//tworzê listê przyciskow za pomoc¹ funkcji CreateCommands
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);//tê listê przypisuje do ReadOnlyCollection (bo readOnlyCollection mo¿na tylko tworzyæ, nie mo¿na do niej dodawaæ)
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()//tu decydujemy jakie przyciski s¹ w lewym menu
        {
			//ten Messenger z³apie wiadomoœæ o otwarciu nowego okna
			//Messenger.Default.Register czeka na stringa i otworzy metodê open. Jak messenger z³apie stringa,
			//który bêdzie zawiera³ polecenie otwarcia okna, to wywo³a metodê open.
			Messenger.Default.Register<string>(this, open);
            //Messenger.Default.Register<string>(this, open);

            var commands = new List<CommandViewModel>();
            return commands;


        }

		private void open(string name)
		
		

		{
			

			switch(name)
			{
				
				case "All Countries":
					showAllCountries();
					break;
				
				
				case "Create Module":
					createView(new CreateModuleViewModel());
					break;
				case "Create Country":
					createView(new CreateCountryViewModel());
					break;
				case "Create Cake Filling":
					createView(new CreateCakeFillingViewModel());
					break;
				case "Create Celebration Cake Size":
					createView(new CreateCelebrationCakeSizeViewModel());
					break;
				case "Create Payment Method":
					createView(new CreatePaymentMethodViewModel());
					break;
				case "Create Product Category":
					createView(new CreateProductCategoryViewModel());
					break;
				

				case "Delete Region":
					createView(new DeleteRegionViewModel());
                    break;

                default: break;
			}
            Messenger.Default.Register<DeleteRegionViewModel>(this, "RegionDelete", OnRegionDeleteMessageReceived);


        }
        #endregion

        #region Zakladki
        private ObservableCollection<WorkspaceViewModel> _Workspaces; //to jest kolekcja zak³adek

        public ObservableCollection<WorkspaceViewModel> Workspaces

        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }
        #endregion



        #region Funkcje pomocnicze

        private void Logout()
        {
            _currentUser.Logout();

            Workspaces.Clear();

            OnPropertyChanged(() => IsUserLoggedIn);

            CommandManager.InvalidateRequerySuggested();
        }

        private void createView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.setActiveWorkspace(workspace);
        }

        

        
		private void createCountry()
		{
			CreateCountryViewModel workspace = new CreateCountryViewModel();
			this.Workspaces.Add(workspace);
			this.setActiveWorkspace(workspace);
		}
		private void createCompanyCode()
		{
			CreateModuleViewModel workspace = new CreateModuleViewModel();
			this.Workspaces.Add(workspace);
			this.setActiveWorkspace(workspace);
		}

		
       

		private void showAllCountries()
		{
			AllCountriesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllCountriesViewModel) as AllCountriesViewModel;
			if (workspace == null)
			{
				workspace = new AllCountriesViewModel();
				this.Workspaces.Add(workspace);
			}
			this.setActiveWorkspace(workspace);
		}
        private void showAllTeamMembers()
        {
            AllTeamMembersViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllTeamMembersViewModel) as AllTeamMembersViewModel;
            if (workspace == null)
            {
                workspace = new AllTeamMembersViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllRegions()
        {
            AllRegionsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllRegionsViewModel) as AllRegionsViewModel;
            if (workspace == null)
            {
                workspace = new AllRegionsViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllMainPageArticles()
        {
            AllMainPageArticlesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllMainPageArticlesViewModel) as AllMainPageArticlesViewModel;
            if (workspace == null)
            {
                workspace = new AllMainPageArticlesViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllInvoices()
        {
            AllInvoicesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllInvoicesViewModel) as AllInvoicesViewModel;
            if (workspace == null)
            {
                workspace = new AllInvoicesViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllModules()
		{
			AllModulesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllModulesViewModel) as AllModulesViewModel;
			if (workspace == null)
			{
				workspace = new AllModulesViewModel();
				this.Workspaces.Add(workspace);
			}
			this.setActiveWorkspace(workspace);
		}

		private void showAllCakeFillings()
		{
			AllCakeFillingsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllCakeFillingsViewModel) as AllCakeFillingsViewModel;
			if (workspace == null)
			{
				workspace = new AllCakeFillingsViewModel();
				this.Workspaces.Add(workspace);
			}
			this.setActiveWorkspace(workspace);
		}

		private void showAllCelebrationCakeSizes()
		{
			AllCelebrationCakeSizesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllCelebrationCakeSizesViewModel) as AllCelebrationCakeSizesViewModel;
			if (workspace == null)
			{
				workspace = new AllCelebrationCakeSizesViewModel();
				this.Workspaces.Add(workspace);
			}
			this.setActiveWorkspace(workspace);
		}

		private void showAllPaymentMethods()
		{
			AllPaymentMethodsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllPaymentMethodsViewModel) as AllPaymentMethodsViewModel;
			if (workspace == null)
			{
				workspace = new AllPaymentMethodsViewModel();
				this.Workspaces.Add(workspace);
			}
			this.setActiveWorkspace(workspace);
		}


		private void showAllProductCategories()
		{
			AllProductCategoriesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllProductCategoriesViewModel) as AllProductCategoriesViewModel;
			if (workspace == null)
			{
				workspace = new AllProductCategoriesViewModel();
				this.Workspaces.Add(workspace);
			}
			this.setActiveWorkspace(workspace);
		}

        private void showAllUserAccesses()
        {
            AllUserAccessViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllUserAccessViewModel) as AllUserAccessViewModel;
            if (workspace == null)
            {
                workspace = new AllUserAccessViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllQuoteRequests()
        {
            AllQuoteRequestsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllQuoteRequestsViewModel) as AllQuoteRequestsViewModel;
            if (workspace == null)
            {
                workspace = new AllQuoteRequestsViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllOrders()
        {
            AllOrdersViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllOrdersViewModel) as AllOrdersViewModel;
            if (workspace == null)
            {
                workspace = new AllOrdersViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllCelebrationCakes()
        {
            AllCelebrationCakesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllCelebrationCakesViewModel) as AllCelebrationCakesViewModel;
            if (workspace == null)
            {
                workspace = new AllCelebrationCakesViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }









        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion
        #region Zakladki innych okien

      
        private void OnRegionDeleteMessageReceived(DeleteRegionViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        private void OnUserAccountDeleteMessageReceived(DeleteUserAccountViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        private void OnCelebrationCakeDeleteMessageReceived(DeleteCelebrationCakeViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        private void OnMainPageArticleDeleteMessageReceived(DeleteMainPageArticleViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        private void OnProductCategoryDeleteMessageReceived(DeleteProductCategoryViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        private void OnQuoteRequestDeleteMessageReceived(DeleteQuoteRequestViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        private void OnOrderDeleteMessageReceived(DeleteOrderViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        private void OnTeamMemberDeleteMessageReceived(DeleteTeamMemberViewModel deleteVm)
        {
            if (deleteVm == null) return;
            createView(deleteVm);
        }
        
        private void OnTeamMemberUpdateMessageReceived(UpdateTeamMemberViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        private void OnMainPageArticleUpdateMessageReceived(UpdateMainPageArticleViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        private void OnQuoteRequestUpdateMessageReceived(UpdateQuoteRequestViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        
        private void OnRegionUpdateMessageReceived(UpdateRegionViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        private void OnUserAccountUpdateMessageReceived(UpdateUserAccountViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        private void OnOrderUpdateMessageReceived(UpdateOrderViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        private void OnCelebrationCakeUpdateMessageReceived(UpdateCelebrationCakeViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        private void OnProductCategoryUpdateMessageReceived(UpdateProductCategoryViewModel updateVM)
        {
            if (updateVM == null) return;
            createView(updateVM);
        }
        private void OnRegionDisplayMessageReceived(DisplayRegionViewModel displayVM)
		{
			if (displayVM == null) return;
			createView(displayVM);
        }
        private void OnCelebrationCakeDisplayMessageReceived(DisplayCelebrationCakeViewModel displayVM)
        {
            if (displayVM == null) return;
            createView(displayVM);
        }
        private void OnMainPageArticleDisplayMessageReceived(DisplayMainPageArticleViewModel displayVM)
        {
            if (displayVM == null) return;
            createView(displayVM);
        }
        private void OnTeamMemberDisplayMessageReceived(DisplayTeamMemberViewModel displayVM)
        {
            if (displayVM == null) return;
            createView(displayVM);
        }
        private void OnProductCategoryDisplayMessageReceived(DisplayProductCategoryViewModel displayVM)
        {
            if (displayVM == null) return;
            createView(displayVM);
        }
        private void OnQuoteRequestDisplayMessageReceived(DisplayQuoteRequestViewModel displayVM)
        {
            if (displayVM == null) return;
            createView(displayVM);
        }
        private void OnOrderDisplayMessageReceived(DisplayOrderViewModel displayVM)
        {
            if (displayVM == null) return;
            createView(displayVM);
        }
        private void OnUserAccountDisplayMessageReceived(DisplayUserAccountViewModel displayVM)
        {
            if (displayVM == null) return;
            createView(displayVM);
        }

        #endregion

        #region Wyrejestrowanie komunikatow
        public void Cleanup()
        {
            Messenger.Default.Unregister<DeleteRegionViewModel>(this, "RegionDelete");
            Messenger.Default.Unregister<DeleteProductCategoryViewModel>(this, "ProductCategoryDelete");
            Messenger.Default.Unregister<UpdateRegionViewModel>(this, "RegionUpdate");
            Messenger.Default.Unregister<UpdateProductCategoryViewModel>(this, "ProductCategoryUpdate");
            Messenger.Default.Unregister<DisplayRegionViewModel>(this, "RegionDisplay");
            Messenger.Default.Unregister<DisplayProductCategoryViewModel>(this, "ProductCategoryDisplay");
            Messenger.Default.Unregister<string>(this); //wyrwestrowanie wszystkich komunikatów typu string
        }

        #endregion

    }
}





