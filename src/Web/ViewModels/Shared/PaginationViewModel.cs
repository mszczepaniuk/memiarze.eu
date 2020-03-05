using System.Collections.Generic;

namespace memiarzeEu.ViewModels.Shared
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int MaxNumberOfPages { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Dictionary<string, string> AllRouteData { get; set; }
        public string AlternativePageName { get; set; }
    }
}
