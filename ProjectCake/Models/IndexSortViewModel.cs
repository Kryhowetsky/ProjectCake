using ProjectCake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Models
{
    public class IndexSortViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel  PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        

    }
}
