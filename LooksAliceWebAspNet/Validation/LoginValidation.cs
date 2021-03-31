using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LooksAliceWebAspNet.Models;

namespace LooksAliceWebAspNet.Validation
{
    public class LoginValidation : ValidationAttribute
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public LoginValidation()
        {
            ErrorMessage = "E-mail Não cadastrado!";
        }

        public override bool IsValid(object value)
        {
            var result = from obj in context.Users select obj;
            if (result.Where(x => x.UserName == value.ToString()).Count() == 0) return false;
            return true;
        }

    }
}