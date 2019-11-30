using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Meme> Memes { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        [MaxLength(100)]
        public string About { get; set; }
    }
}