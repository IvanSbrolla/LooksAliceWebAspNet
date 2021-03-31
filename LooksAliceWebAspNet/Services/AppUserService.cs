using LooksAliceWebAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class AppUserService
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public string PrimeiroNome { get; set; }
        public string GetPrimeiroNome(string username)
        {
            var result = from obj in context.Users select obj;
            return result
                .Where(x => x.UserName == username)
                .Select(x => x.PrimeiroNome)
                .FirstOrDefault();
        }
        public ApplicationUser GetInfosUser(string UserName)
        {
            var result = from obj in context.Users select obj;
            return result
                .Where(x => x.UserName == UserName)
                .FirstOrDefault();
        }
    }
}