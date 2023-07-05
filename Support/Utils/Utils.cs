using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Web.Tests.Support.Utils
{
    public static class Utils
    {
        public static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            
            string randomString = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            
            return randomString;
        }

        public static string GenerateRandomNumber(int length)
        {
            const string chars = "0123456789";
            var random = new Random();

            string randomNumber = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomNumber;
        }

        public static string GenerateRandomEmail()
        {
            string email = GenerateRandomString(5) + "." + GenerateRandomString(8) + "@testautomation.co.nz";
            return email;
        }
    }
}
