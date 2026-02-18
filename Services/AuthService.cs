using DesktopApp.Helpers;
using MakeAWishDB.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DesktopApp.Services
{
    public class AuthService
    {
        private readonly SharedData_Entities _db;

        public AuthService(SharedData_Entities db)
        {
            _db = db;
        }

        public AuthenticatedUser Authenticate(string email, string password)
        {
            var user = _db.UserAccounts
                .Include(u => u.UserRoleNavigation)
                    .ThenInclude(r => r.ModuleAccesses)
                        .ThenInclude(ma => ma.Module)
                .FirstOrDefault(u =>
                    u.Email == email &&
                    u.IsActive);

            if (user == null)
                return null;

            // hashowanie hasla w celu porownania z tym przechowywanym w bazie danych
            var hashedPassword = PasswordHasher.Hash(password);

            if (user.PasswordHash != hashedPassword)
                return null;

            var allowedModules = user.UserRoleNavigation.ModuleAccesses
                .Where(ma => ma.HasAccess && ma.IsActive && ma.Module.IsActive)
                .Select(ma => ma.Module.Code)
                .ToList();

            return new AuthenticatedUser
            {
                UserId = user.UserId,
                Email = user.Email,
                DisplayName = user.DisplayName,
                RoleName = user.UserRoleNavigation.RoleName,
                AllowedModuleCodes = allowedModules
            };
        }

    }
}
