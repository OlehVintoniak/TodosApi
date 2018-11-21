using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Todo.Models.Abstract;

namespace WebApi.Todo.Models
{
    public class TodoItem : Entity<long>
    {
        public string Description { get; set; }

        public TodoItemStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        #region Keys

        public static void SetupKeys(ModelBuilder modelBuilder)
        {
        }

        public static void SetupRelations(ModelBuilder modelBuilder)
        {
        }

        #endregion

    }

    public enum TodoItemStatus
    {
        New,
        Done
    }
}
