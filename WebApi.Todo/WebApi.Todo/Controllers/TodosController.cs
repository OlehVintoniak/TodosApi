using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Todo.Interfaces;
using WebApi.Todo.ViewModels;

namespace WebApi.Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _todoService.GetAllTodoItemsByCurrentUser();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TodoItemViewModel model)
        {
            var response = await _todoService.CreateTodoItem(model);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]TodoItemViewModel model)
        {
            var response = await _todoService.UpdateTodoItem(model);
            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _todoService.DeleteTodoItemById(id);
            return NoContent();
        }
    }
}