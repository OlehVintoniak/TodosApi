using WebApi.Todo.Models;

namespace WebApi.Todo.ViewModels
{
    public class ChangeStatusViewModel
    {
        public long TodoItemId { get; set; }
        public TodoItemStatus Status { get; set; }
    }
}
