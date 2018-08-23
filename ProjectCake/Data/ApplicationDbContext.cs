using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectCake.Maps;
using ProjectCake.Models;

namespace ProjectCake.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new OrderCakeMap(builder.Entity<OrderCake>());
            new ProductMap(builder.Entity<Product>());
            new CategoryMap(builder.Entity<Category>());
            new FileMap(builder.Entity<File>());
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ProjectCake.Models.OrderCake> OrderCake { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}