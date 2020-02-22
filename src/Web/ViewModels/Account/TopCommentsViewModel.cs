﻿using memiarzeEu.Models;
using memiarzeEu.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels.Account
{
    public class TopCommentsViewModel
    {
        public ApplicationUser User { get; set; }
        public ICollection<CommentViewModel> CommentViewModels { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
