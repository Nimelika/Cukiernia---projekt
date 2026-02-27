using DesktopApp.Helpers;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.UserAccountVM
{
    public class CreateUserAccountViewModel : CreateViewModel<UserAccount>
    {
        public CreateUserAccountViewModel()
            : base("Add User Access")
        {
        }

        protected override void InitializeNewItem()
        {
            item.IsActive = true;
            item.CreatedAt = DateTime.Now;
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

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(() => Password); }
        }

        public int UserRole
        {
            get => item.UserRole;
            set { item.UserRole = value; OnPropertyChanged(() => UserRole); }
        }

        public ObservableCollection<UserRole> Roles =>
            new(sharedData_Entities.UserRoles.Where(r => r.IsActive));

        public override bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Email)) return false;
            if (string.IsNullOrWhiteSpace(Password)) return false;

            item.PasswordHash = PasswordHasher.Hash(Password);
            return true;
        }
    }

}
