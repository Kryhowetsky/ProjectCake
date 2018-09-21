using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Models
{
    public class IndexSortOrderListViewModel
    {
        public  SortOrderListViewModel SortOrderListViewModel { get; set; }
        public IEnumerable<OrderCake> OrderCake { get; set; }
        //public OrderCake OrderCake { get; internal set; }
    }
}
