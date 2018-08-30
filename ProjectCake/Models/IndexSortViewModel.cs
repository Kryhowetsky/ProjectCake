using ProjectCake.Data;
using System.Collections.Generic;

namespace ProjectCake.Models
{
    public class IndexSortViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}