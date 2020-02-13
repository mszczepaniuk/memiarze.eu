using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.Models;

namespace memiarzeEu.ViewModels
{
    public class AccountIndexViewModel
    {
        public ApplicationUser ProfileOwner { get; set; }
        public ICollection<Meme> Memes { get; set; }
        public string PartialViewName { set; get; }
    }
}
