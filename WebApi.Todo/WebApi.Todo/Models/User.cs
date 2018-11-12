using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Todo.Models.Abstract;

namespace WebApi.Todo.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser<long>, IEntity<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();

        public ICollection<TodoItem> TodoItems { get; } = new List<TodoItem>();

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        #region Keys

        public static void SetupKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id)
                .ForSqlServerIsClustered();
        }

        public static void SetupRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.TodoItems)
                .WithOne(ti => ti.User)
                .HasForeignKey(ti => ti.UserId);
        }

        #endregion
    }
}
