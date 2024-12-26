using Microsoft;
using Microsoft.EntityFrameworkCore;
using ShgardiProductAPI.Repository.Models;

namespace ProductApi.Repository.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}