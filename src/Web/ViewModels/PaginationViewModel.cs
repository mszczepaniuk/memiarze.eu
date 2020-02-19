using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int MaxNumberOfPages { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Dictionary<string, string> AllRouteData {get; set;}
        public string AlternativePageName { get; set; }
    }
}
