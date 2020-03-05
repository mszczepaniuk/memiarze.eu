using memiarzeEu.Models;
using memiarzeEu.ViewModels.Shared;
using System.Collections.Generic;

namespace memiarzeEu.ViewModels.Account
{
    public class TopMemesViewModel
    {
        public ApplicationUser User { get; set; }
        public ICollection<MemeCardViewModel> MemeCardViewModels { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
