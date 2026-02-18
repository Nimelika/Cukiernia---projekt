using BusinessLogic.Models.EntitiesForView;
using DesktopApp.ViewModels.AbstractViewModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllUserAccessViewModel : AllViewModel<UserAccessForView>
    {
        public AllUserAccessViewModel()
            : base("User Access")
        {
            DetailsCommand = new RelayCommand<UserAccessForView>(ShowDetails);
            UpdateCommand = new RelayCommand<UserAccessForView>(ShowUpdate);
            Load();
        }

        public RelayCommand<UserAccessForView> DetailsCommand { get; }
        public RelayCommand<UserAccessForView> UpdateCommand { get; }

        private UserAccessForView _selectedUser;
        public UserAccessForView SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    Messenger.Default.Send(_selectedUser);
                }
            }
        }

        public override void Load()
        {
            List = new ObservableCollection<UserAccessForView>(
                SharedData_Entities.UserAccounts
                    .Where(u => u.IsActive)
                    .Include(u => u.UserRoleNavigation)
                    .Include(u => u.UserRoleNavigation.ModuleAccesses)
                    .ThenInclude(ma => ma.Module)
                    .AsEnumerable()
                    .Select((u, index) => new UserAccessForView
                    {
                        Position = index + 1,
                        UserId = u.UserId,
                        Email = u.Email,
                        DisplayName = u.DisplayName,
                        RoleName = u.UserRoleNavigation.RoleName,
                        Modules = string.Join(", ",
                            u.UserRoleNavigation.ModuleAccesses
                                .Where(ma => ma.HasAccess)
                                .Select(ma => ma.Module.Code))
                    })
            );
        }

        private void ShowDetails(UserAccessForView user)
        {
         //   if (user == null) return;

          //  var vm = new DisplayUserAccessViewModel();
          //  vm.Load(user.UserId);
            //Messenger.Default.Send(vm, "UserAccessDisplay");
        }

        private void ShowUpdate(UserAccessForView user)
        {
           // if (user == null) return;

           // var vm = new UpdateUserAccessViewModel();
           // vm.Load(user.UserId);
            //Messenger.Default.Send(vm, "UserAccessUpdate");
        }
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Email", "Name", "Role" };
        }

        public override void Sort()
        {
            if (SortField == "Email")
                List = new ObservableCollection<UserAccessForView>(List.OrderBy(u => u.Email));

            if (SortField == "Name")
                List = new ObservableCollection<UserAccessForView>(List.OrderBy(u => u.DisplayName));

            if (SortField == "Role")
                List = new ObservableCollection<UserAccessForView>(List.OrderBy(u => u.RoleName));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Email", "Name", "Role" };
        }

        public override void Find()
        {
            base.Find();

            if (FindField == "Email")
                List = new ObservableCollection<UserAccessForView>(
                    List.Where(u => u.Email != null &&
                                    u.Email.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));

            if (FindField == "Name")
                List = new ObservableCollection<UserAccessForView>(
                    List.Where(u => u.DisplayName != null &&
                                    u.DisplayName.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));

            if (FindField == "Role")
                List = new ObservableCollection<UserAccessForView>(
                    List.Where(u => u.RoleName != null &&
                                    u.RoleName.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
        }

    }

}

