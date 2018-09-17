using ProjectCake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Models
{
    public class IndexDetailProdRespondViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public RespondViewModel RespondViewModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
    }
}
