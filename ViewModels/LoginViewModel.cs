using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamietaj mnie")]
        public bool RememberMe { get; set; }
    }
}
