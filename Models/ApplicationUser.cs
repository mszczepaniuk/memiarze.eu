using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Meme> Memes { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
