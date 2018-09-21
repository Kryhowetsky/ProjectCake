using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Models
{
    public class SortOrderListViewModel
    {
        public SortStateOrderList NameSort { get; private set; }
        public SortStateOrderList SurnameSort { get; private set; }
        public SortStateOrderList PreparedOrderDateSort { get; private set; }
        public SortStateOrderList DateSort { get; private set; }
        public SortStateOrderList Current { get; private set; }

        public SortOrderListViewModel(SortStateOrderList sortOrderList)
        {
            NameSort = sortOrderList == SortStateOrderList.NameAsc ? SortStateOrderList.NameDesc : SortStateOrderList.NameAsc;
            SurnameSort = sortOrderList == SortStateOrderList.SurnameAsc ? SortStateOrderList.SurnameDesc : SortStateOrderList.SurnameAsc;
            PreparedOrderDateSort = sortOrderList == SortStateOrderList.PreparedOrderDateAsc ? SortStateOrderList.PreparedOrderDateDesc : SortStateOrderList.PreparedOrderDateAsc;
            DateSort = sortOrderList == SortStateOrderList.DateAsc ? SortStateOrderList.DateDesc : SortStateOrderList.DateAsc;
            Current = sortOrderList;
        }
    }
}
