
namespace ProjectCake.Data
{
    public class Respond : BaseEntity
    {

        public string Name { get; set; }

        public string Email { get; set; }
        public string Text { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
