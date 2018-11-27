using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Todo.Models;
using WebApi.Todo.ViewModels;

namespace WebApi.Todo.Interfaces
{
    public interface ITodoService
    {
        Task<List<TodoItemViewModel>> GetAllTodoItemsByCurrentUser();

        Task<TodoItemViewModel> CreateTodoItem(TodoItemViewModel model);

        Task<TodoItemViewModel> UpdateTodoItem(TodoItemViewModel model);

        Task<TodoItemViewModel> ChangeStatus(long id, TodoItemStatus status);

        Task DeleteTodoItemById(long id);
    }
}