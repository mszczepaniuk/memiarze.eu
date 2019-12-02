using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels
{
    public class AdminIndexViewModel
    {
        public string UserName { get; set; }
        public ICollection<ApplicationUser> Admins { get; set; }
    }
}
