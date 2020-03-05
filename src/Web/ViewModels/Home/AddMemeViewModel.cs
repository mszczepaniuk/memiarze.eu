using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace memiarzeEu.ViewModels.Home
{
    public class AddMemeViewModel
    {
        [Required(ErrorMessage = "Podaj tytuł")]
        [MaxLength(100, ErrorMessage = "Tytuł może mieć maksymalnie 100 znaków")]
        [Display(Name = "Tytul")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Podaj obraz")]
        public IFormFile Image { get; set; }
    }
}
