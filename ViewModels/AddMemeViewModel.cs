using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels
{
    public class AddMemeViewModel
    {
        [Required]
        [Display(Name = "Tytul")]
        public string Title { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
