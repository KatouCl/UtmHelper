using System.Collections.Generic;

namespace Utm.WebApplication.ViewModel.Mark
{
    public class MarkListingVm
    {
        public string SearchMark { get; set; }
        public IEnumerable<Domain.Mark> Marks { get; set; }
    }
}