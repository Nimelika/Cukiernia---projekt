using DesktopApp.Helpers;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.UserAccountVM
{
    public class UpdateUserAccountViewModel : UpdateViewModel<UserAccount>
    {
        public UpdateUserAccountViewModel()
            : base("Edit User")
        {
        }

        public override void Load(int id)
        {
            item = sharedData_Entities.UserAccounts
                .Include(u => u.UserRoleNavigation)
                .First(u => u.UserId == id);
        }

        public string Email
        {
            get => item.Email;
            set { item.Email = value; OnPropertyChanged(() => Email); }
        }

        public string DisplayName
        {
            get => item.DisplayName;
            set { item.DisplayName = value; OnPropertyChanged(() => DisplayName); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(() => NewPassword); }
        }

        public int UserRole
        {
            get => item.UserRole;
            set { item.UserRole = value; OnPropertyChanged(() => UserRole); }
        }

        public ObservableCollection<UserRole> Roles =>
            new(sharedData_Entities.UserRoles.Where(r => r.IsActive));

        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(NewPassword) &&
                !string.IsNullOrWhiteSpace(NewPassword))
            {
                item.PasswordHash = PasswordHasher.Hash(NewPassword);
            }
            return null;
        }
    }

}
