using memiarzeEu.Models;
using System.Collections.Generic;

namespace memiarzeEu.ViewModels.Account
{
    public class AccountIndexViewModel
    {
        public ApplicationUser ProfileOwner { get; set; }
        public ICollection<Meme> Memes { get; set; }
        public string PartialViewName { set; get; }
    }
}
