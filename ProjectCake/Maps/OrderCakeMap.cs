using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Maps
{
    public class OrderCakeMap
    {
        public OrderCakeMap(EntityTypeBuilder<OrderCake> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
        
    }
}
