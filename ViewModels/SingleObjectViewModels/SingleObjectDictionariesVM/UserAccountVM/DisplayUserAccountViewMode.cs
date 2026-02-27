using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.UserAccountVM
{
    public class DisplayUserAccountViewModel : DisplayViewModel<UserAccount>
    {
        public DisplayUserAccountViewModel()
            : base("User Details")
        {
        }

        public void Load(int id)
        {
            item = sharedData_Entities.UserAccounts
                .Include(u => u.UserRoleNavigation)
                .ThenInclude(r => r.ModuleAccesses)
                .ThenInclude(ma => ma.Module)
                .First(u => u.UserId == id);
        }

        public string Email => item.Email;
        public string DisplayName => item.DisplayName;
        public string RoleName => item.UserRoleNavigation?.RoleName;

        public string Modules =>
            string.Join(", ",
                item.UserRoleNavigation.ModuleAccesses
                    .Where(ma => ma.HasAccess)
                    .Select(ma => ma.Module.Code));
    }


}
