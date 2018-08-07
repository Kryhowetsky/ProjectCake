using System;

namespace ProjectCake.Models
{
    public class OrderCake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
    }
}