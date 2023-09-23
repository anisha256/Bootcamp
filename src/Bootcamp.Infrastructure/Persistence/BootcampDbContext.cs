using Bootcamp.Domain.Entities;
using Bootcamp.WebAPI.Modals;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

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

            builder.Entity<CategoryItem>()
            .HasKey(ci => ci.Id);

            builder.Entity<CategoryItem>()
                .HasOne(ci => ci.Category)
                .WithMany(c => c.CategoryItems)
                .HasForeignKey(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CategoryItem>()
                .HasOne(ci => ci.Item)
                .WithMany(i => i.CategoryItems)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.NoAction);




        }


    }
}
