using memiarzeEu.ViewModels.Shared;
using System.Collections.Generic;

namespace memiarzeEu.ViewModels.Home
{
    public class IndexViewModel
    {
        public ICollection<MemeCardViewModel> MemeCardViewModels { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
