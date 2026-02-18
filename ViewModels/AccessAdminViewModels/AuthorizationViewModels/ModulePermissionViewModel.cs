using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.AccessAdminViewModels.AuthorizationViewModels
{
    public class ModulePermissionViewModel
    {
        public string Code { get; }
        public string Name { get; }
        public bool HasAccess { get; }

        public ModulePermissionViewModel(string code, string name, bool hasAccess)
        {
            Code = code;
            Name = name;
            HasAccess = hasAccess;
        }
    }

}
