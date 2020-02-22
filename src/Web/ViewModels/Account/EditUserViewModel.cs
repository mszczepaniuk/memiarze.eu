using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
