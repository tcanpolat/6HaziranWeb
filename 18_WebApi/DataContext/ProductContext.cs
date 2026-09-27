using _18_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace _18_WebApi.DataContext
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
