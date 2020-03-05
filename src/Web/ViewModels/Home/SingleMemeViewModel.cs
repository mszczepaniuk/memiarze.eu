using memiarzeEu.ViewModels.Shared;
using System.Collections.Generic;

namespace memiarzeEu.ViewModels.Home
{
    public class SingleMemeViewModel
    {
        public MemeCardViewModel MemeCardViewModel { get; set; }
        public ICollection<CommentViewModel> CommentViewModels { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
