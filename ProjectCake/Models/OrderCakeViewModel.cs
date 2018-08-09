using Microsoft.AspNetCore.Http;
using System;

namespace ProjectCake.Models
{
    public class OrderCakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public IFormFile File { get; set; }
    }
}