using System;
using WebApi.Todo.Models;

namespace WebApi.Todo.ViewModels
{
    public class TodoItemViewModel
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public TodoItemStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
