using memiarzeEu.Models;
using memiarzeEu.ViewModels.Shared;
using System.Collections.Generic;

namespace memiarzeEu.ViewModels.Account
{
    public class TopCommentsViewModel
    {
        public ApplicationUser User { get; set; }
        public ICollection<CommentViewModel> CommentViewModels { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
