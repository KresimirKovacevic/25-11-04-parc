using IspitTodo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = IspitTodo.Models.Task;

namespace IspitTodo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        #region Domain entities

        public DbSet<Todolist> Todolists { get; set; }

        public DbSet<Task> Tasks { get; set; }

        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<ApplicationUser>()
                .HasMany(u => u.Todolists)
                .WithOne(l => l.User);

            builder.Entity<Todolist>()
                .HasMany(l => l.Tasks)
                .WithOne(t => t.Todolist);*/

            builder.Entity<Task>()
                .HasOne(t => t.Todolist)
                .WithMany()
                .HasForeignKey(t => t.TodolistId);

            builder.Entity<Todolist>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);

            /*builder.Entity<Category>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<OrderItem>()
                .HasIndex(x => new { x.OrderId, x.ProductId })
                .IsUnique();*/

            /*builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = "A28AD2C3-EE88-4024-8517-D2749CF78914",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                });

            builder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser
                {
                    Id = "697BAC82-B48C-4F17-AD01-137322F28558",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",

                });*/

            base.OnModelCreating(builder);
        }
    }
}
