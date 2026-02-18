using System.Collections.Generic;

namespace DesktopApp.Services
{
    public class AuthenticatedUser
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public IReadOnlyCollection<string> AllowedModuleCodes { get; set; }
    }
}
