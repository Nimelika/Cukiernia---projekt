using DesktopApp.ViewModels.AbstractViewModels;
using GalaSoft.MvvmLight.Command;
using MakeAWishDB.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DesktopApp.ViewModels.AccessAdminViewModels.RoleAccessViewModels
{
    public class ManageRoleModulesViewModel : WorkspaceViewModel
    {
        private readonly MakeAWishDB.Context.SharedData_Entities _db;

        public ManageRoleModulesViewModel()
        {
            DisplayName = "Role Module Access";
            _db = new MakeAWishDB.Context.SharedData_Entities();

            SaveCommand = new RelayCommand(Save, CanSave);

            LoadRoles();
        }

        #region Roles

        public ObservableCollection<UserRole> Roles { get; private set; }

        private UserRole _selectedRole;
        public UserRole SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    OnPropertyChanged(() => SelectedRole);

                    LoadModulesForRole();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private void LoadRoles()
        {
            Roles = new ObservableCollection<UserRole>(
                _db.UserRoles
                   .Where(r => r.IsActive)
                   .OrderBy(r => r.RoleName));

            OnPropertyChanged(() => Roles);

            SelectedRole = Roles.FirstOrDefault();
        }

        #endregion

        #region Modules

        public ObservableCollection<ModuleAccessForView> Modules { get; private set; }

        private void LoadModulesForRole()
        {
            if (SelectedRole == null)
            {
                Modules = new ObservableCollection<ModuleAccessForView>();
                OnPropertyChanged(() => Modules);
                SaveCommand.RaiseCanExecuteChanged();
                return;
            }

            var allModules = _db.Modules
                .Where(m => m.IsActive)
                .OrderBy(m => m.ModuleName)
                .ToList();

            var roleAccesses = _db.ModuleAccesses
                .Where(ma => ma.RoleId == SelectedRole.RoleId)
                .ToList();

            Modules = new ObservableCollection<ModuleAccessForView>(
                allModules.Select(m =>
                {
                    var access = roleAccesses.FirstOrDefault(a => a.ModuleId == m.ModuleId);

                    return new ModuleAccessForView
                    {
                        ModuleId = m.ModuleId,
                        ModuleName = m.ModuleName,
                        HasAccess = access?.HasAccess ?? false
                    };
                })
            );

            OnPropertyChanged(() => Modules);
            SaveCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Save

        public RelayCommand SaveCommand { get; }

        private bool CanSave()
        {
            return SelectedRole != null && Modules != null && Modules.Any();
        }

        private void Save()
        {
            if (!CanSave())
                return;

            foreach (var module in Modules)
            {
                var access = _db.ModuleAccesses
                    .FirstOrDefault(a =>
                        a.RoleId == SelectedRole.RoleId &&
                        a.ModuleId == module.ModuleId);

                if (access == null)
                {
                    access = new ModuleAccess
                    {
                        RoleId = SelectedRole.RoleId,
                        ModuleId = module.ModuleId,
                        HasAccess = module.HasAccess,
                        IsActive = true
                    };
                    _db.ModuleAccesses.Add(access);
                }
                else
                {
                    access.HasAccess = module.HasAccess;
                }
            }

            _db.SaveChanges();

            MessageBox.Show(
                "Role access has been updated.\nChanges will apply after re-login.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        #endregion
    }
}

