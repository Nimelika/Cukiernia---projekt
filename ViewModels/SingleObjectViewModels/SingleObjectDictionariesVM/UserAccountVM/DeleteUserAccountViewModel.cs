using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.UserAccountVM
{
    public class DeleteUserAccountViewModel : DeleteViewModel<UserAccount>
    {
        public DeleteUserAccountViewModel()
            : base("Delete User")
        {
        }

        protected override string RefreshMessageTag => "User Access";

        public string Email => item?.Email;
        public string DisplayName => item?.DisplayName;
        public string RoleName => item.UserRoleNavigation?.RoleName;
    }

}
