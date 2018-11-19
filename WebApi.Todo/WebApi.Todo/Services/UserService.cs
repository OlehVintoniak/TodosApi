using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Todo.Database;
using WebApi.Todo.Interfaces;
using WebApi.Todo.Models;

namespace WebApi.Todo.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
