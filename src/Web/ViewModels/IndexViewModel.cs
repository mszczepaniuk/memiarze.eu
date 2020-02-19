using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels
{
    public class IndexViewModel
    {
        public ICollection<MemeCardViewModel> MemeCardViewModels { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
