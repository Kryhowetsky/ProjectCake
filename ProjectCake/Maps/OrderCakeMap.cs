using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCake.Models;

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