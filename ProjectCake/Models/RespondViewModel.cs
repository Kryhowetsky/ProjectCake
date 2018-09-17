using ProjectCake.Data;
using System;


namespace ProjectCake.Models
{
    public class RespondViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
