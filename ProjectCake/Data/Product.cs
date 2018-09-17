
using System;
using System.Collections.Generic;

namespace ProjectCake.Data
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public string ImageProd { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Respond> Respond { get; set; }
        public Product()
        {
            Respond = new List<Respond>();
        }
    }
}