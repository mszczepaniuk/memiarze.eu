using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace memiarzeEu.Validation
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        private readonly PasswordOptions passwordOptions;

        public PasswordValidationAttribute()
        {
            var identityOptions = PasswordOptionsStore.IdentityOptions;
            passwordOptions = identityOptions.Password;
            ErrorMessage = "";
        }

        public override bool IsValid(object value)
        {
            string password = value.ToString();
            bool returnValue = true;

            if (passwordOptions.RequireDigit && !Regex.IsMatch(password, @"\d"))
            {
                ErrorMessage += ("Hasło powinno posiadać przynajmniej jedną cyfrę." + Environment.NewLine);
                returnValue = false;
            }
            if (passwordOptions.RequireNonAlphanumeric && !Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
            {
                ErrorMessage += ("Hasło powinno posiadać przynajmniej jeden znak niealfanumeryczny." + Environment.NewLine);
                returnValue = false;
            }
            if (passwordOptions.RequireLowercase && !Regex.IsMatch(password, @"[^a-z]"))
            {
                ErrorMessage += ("Hasło powinno posiadać przynajmniej jedną małą litere." + Environment.NewLine);
                returnValue = false;
            }
            if (passwordOptions.RequireUppercase && !Regex.IsMatch(password, @"[^A-Z]"))
            {
                ErrorMessage += ("Hasło powinno posiadać przynajmniej jedną wielką litere." + Environment.NewLine);
                returnValue = false;
            }
            if (passwordOptions.RequiredLength > password.Length)
            {
                ErrorMessage += ("Hasło jest za krótkie." + Environment.NewLine);
                returnValue = false;
            }
            if (passwordOptions.RequiredUniqueChars > password.Distinct().Count())
            {
                ErrorMessage += ($"Hasło powinno posiadać przynajmniej {passwordOptions.RequiredUniqueChars} unikalnych znaków." + Environment.NewLine);
                returnValue = false;
            }

            return returnValue;
        }
    }
}
