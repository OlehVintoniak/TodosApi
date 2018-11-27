using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Todo.Database;
using WebApi.Todo.Interfaces;
using WebApi.Todo.Models;
using WebApi.Todo.ViewModels;

namespace WebApi.Todo.Services
{
    public class TodoItemService : ITodoService
    {
        private readonly AppDbContext _context;
        private readonly IUserWrapper _userWrapper;

        public TodoItemService(AppDbContext context, IUserWrapper userWrapper)
        {
            _context = context;
            _userWrapper = userWrapper;
        }

        public async Task<List<TodoItemViewModel>> GetAllTodoItemsByCurrentUser()
        {
            return await _context.TodoItems
                .AsNoTracking()
                .Where(tdi => tdi.UserId == _userWrapper.Id)
                .Select(t => new TodoItemViewModel
                {
                    Id = t.Id,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt,
                    Status = t.Status

                }).ToListAsync();
        }

        public async Task<TodoItemViewModel> CreateTodoItem(TodoItemViewModel model)
        {
            var todoItemToCreate = new TodoItem
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Status = TodoItemStatus.New,
                UserId = _userWrapper.Id
            };
            _context.TodoItems.Add(todoItemToCreate);
            await _context.SaveChangesAsync();
            return TransformToView(todoItemToCreate);
        }

        public async Task<TodoItemViewModel> UpdateTodoItem(TodoItemViewModel model)
        {
            var todoItemToUpdate = await GetTodoItemOfCurrentUserById(model.Id);

            if (!string.IsNullOrWhiteSpace(model.Description))
            {
                todoItemToUpdate.Description = model.Description;
            }
            await _context.SaveChangesAsync();
            return TransformToView(todoItemToUpdate);
        }

        public async Task<TodoItemViewModel> ChangeStatus(long id, TodoItemStatus status)
        {
            var todoItemToChangeStatus = await GetTodoItemOfCurrentUserById(id);

            if (Enum.IsDefined(typeof(TodoItemStatus), status))
            {
                todoItemToChangeStatus.Status = status;
            }
            else
            {
                throw new Exception("Status not defined");
            }
            await _context.SaveChangesAsync();
            return TransformToView(todoItemToChangeStatus);
        }

        public async Task DeleteTodoItemById(long id)
        {
            var todoItemToDelete = await GetTodoItemOfCurrentUserById(id);
            _context.TodoItems.Remove(todoItemToDelete);
            await _context.SaveChangesAsync();
        }

        private TodoItemViewModel TransformToView(TodoItem todoItem)
        {
            return new TodoItemViewModel
            {
                CreatedAt = todoItem.CreatedAt,
                Id = todoItem.Id,
                Description = todoItem.Description,
                Status = todoItem.Status
            };
        }

        private async Task<TodoItem> GetTodoItemOfCurrentUserById(long id)
        {
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == _userWrapper.Id);
            if (todoItem == null)
            {
                throw new Exception("Todo item was not found");
            }
            return todoItem;
        }
    }
}
