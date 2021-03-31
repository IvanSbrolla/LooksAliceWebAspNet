using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LooksAliceWebAspNet.Models;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Validation
{
    public class EmailValidation : ValidationAttribute
    {
        
        ApplicationDbContext context = new ApplicationDbContext();

        public EmailValidation()
        {
            ErrorMessage = "E-mail ja cadastrado!";
        }

        public override bool IsValid(object value)
        {
            var result = from obj in context.Users select obj;
            if (result.Where(x => x.UserName == value.ToString()).Count() == 0)
            {
                return true;
            }
            else
                return false;

        }
    }
}