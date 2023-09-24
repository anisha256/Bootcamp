using Bootcamp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bootcamp.Infrastructure.Persistence
{
    public class BootcampDbContext : DbContext
    {
        public BootcampDbContext(DbContextOptions<BootcampDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CategoryItem> CategoriesItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasQueryFilter(e => e.DeleteFlag == false);
            builder.Entity<Item>().HasQueryFilter(e => e.DeleteFlag == false);
            builder.Entity<CategoryItem>().HasQueryFilter(e => e.DeleteFlag == false);


        }


    }
}
