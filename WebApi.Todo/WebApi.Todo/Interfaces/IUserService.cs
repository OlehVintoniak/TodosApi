using System.Threading.Tasks;
using WebApi.Todo.Models;

namespace WebApi.Todo.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByEmailAsync(string email);
    }
}