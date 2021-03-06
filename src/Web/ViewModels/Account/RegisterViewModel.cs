﻿using memiarzeEu.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace memiarzeEu.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Podaj nazwe użytkownika.")]
        [Display(Name = "Nazwa użytkownika")]
        [Remote(action: "IsUsernameTaken", controller: "Account")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Podaj hasło.")]
        [PasswordValidation]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password",
            ErrorMessage = "Hasła do siebie nie pasują.")]
        public string ConfirmPassword { get; set; }
    }
}
