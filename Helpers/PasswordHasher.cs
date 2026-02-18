using System;
using System.Security.Cryptography;
using System.Text;

namespace DesktopApp.Helpers
{
    public static class PasswordHasher
    {
        //zamiana hasla na string przy uzyciu funkcji SHA‑256, co zapewnia przechowanie hasla jako hash
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}

