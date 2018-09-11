using ProjectCake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Models
{
    public class IndexDetailProdRespondViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<RespondViewModel> RespondViewModel { get; set; }
        public IEnumerable<ProductViewModel> ProductViewModel { get; internal set; }
    }
}
