using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace memiarzeEu.ViewModels.Account
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [MaxLength(100)]
        public string About { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
