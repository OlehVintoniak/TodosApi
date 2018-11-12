using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Todo.Models;

namespace WebApi.Todo.Database
{
    public class AppDbContext : IdentityDbContext<User, Role, long>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            User.SetupKeys(builder);
            UserRole.SetupKeys(builder);

            User.SetupRelations(builder);
            UserRole.SetupRelations(builder);
        }
    }
}
