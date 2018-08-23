using Microsoft.AspNetCore.Http;
using ProjectCake.Data;
using System;
using System.Collections.Generic;


namespace ProjectCake.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public string ImageProd { get; set; }
        public IFormFile File { get; set; }

        public int CategoryId { get; set; }
        //public string CategoryName { get; set; } 
        public Category Category { get; set; }
        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}