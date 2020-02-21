using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podaj nazwe użytkownika.")]
        [Display(Name="Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Podaj hasło.")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamietaj mnie")]
        public bool RememberMe { get; set; }
    }
}
