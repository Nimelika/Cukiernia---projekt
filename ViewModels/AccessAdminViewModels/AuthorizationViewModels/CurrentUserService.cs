using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels
{
    public class CurrentUserService
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string? DisplayName { get; set; }
        public string RoleName { get; set; }
        public bool IsLoggedIn => UserId > 0;


        public IReadOnlyCollection<string> AllowedModuleCodes { get; set; }

        public void Logout()
        {
            UserId = 0;
            Email = null;
            DisplayName = null;
            RoleName = null;
            AllowedModuleCodes = Array.Empty<string>();
        }

    }

}
