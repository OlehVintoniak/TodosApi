using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApi.Todo.Constants;
using WebApi.Todo.Models;

namespace WebApi.Todo.Database
{
    public class Seeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public Seeder(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            var adminEmail = "admin@gmail.com";
            var password = "1z1z1zZ_";
            if (await _roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await _roleManager.CreateAsync(new Role{ Name = Roles.Admin});
            }
            if (await _roleManager.FindByNameAsync(Roles.User) == null)
            {
                await _roleManager.CreateAsync(new Role { Name = Roles.User });
            }
            if (await _userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "System",
                    LastName = "Admin"
                };
                var result = await _userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, Roles.Admin);
                    await _userManager.AddToRoleAsync(admin, Roles.User);
                }
            }
        }
    }
}

