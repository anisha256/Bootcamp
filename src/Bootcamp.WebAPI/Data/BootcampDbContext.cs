using Bootcamp.WebAPI.Modals;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.WebAPI.Data
{
    public class BootcampDbContext : DbContext
    {

        public BootcampDbContext(DbContextOptions<BootcampDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }


    }
}
