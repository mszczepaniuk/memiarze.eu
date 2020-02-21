using memiarzeEu.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels.Home
{
    public class SingleMemeViewModel
    {
        public MemeCardViewModel MemeCardViewModel { get; set; }
        public ICollection<CommentViewModel> CommentViewModels { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
