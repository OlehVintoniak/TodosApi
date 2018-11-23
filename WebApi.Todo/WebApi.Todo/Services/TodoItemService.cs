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

            model.Id = todoItemToCreate.Id;
            model.CreatedAt = todoItemToCreate.CreatedAt;
            return model;
        }

        public async Task<TodoItemViewModel> UpdateTodoItem(TodoItemViewModel model)
        {
            var todoItemToUpdate = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == model.Id);
            if (todoItemToUpdate == null)
            {
                throw new Exception("Todo item was not found");
            }

            if (!string.IsNullOrWhiteSpace(model.Description))
            {
                todoItemToUpdate.Description = model.Description;
            }

            await _context.SaveChangesAsync();

            return new TodoItemViewModel
            {
                CreatedAt = todoItemToUpdate.CreatedAt,
                Id = todoItemToUpdate.Id,
                Description = todoItemToUpdate.Description,
                Status = todoItemToUpdate.Status
            };
        }

        public async Task DeleteTodoItemById(long id)
        {
            var todoItemToDelete = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (todoItemToDelete == null)
            {
                throw new Exception("Todo item was not found");
            }

            _context.TodoItems.Remove(todoItemToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
