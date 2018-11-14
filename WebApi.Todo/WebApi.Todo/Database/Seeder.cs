using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Todo.Constants;
using WebApi.Todo.Models;

namespace WebApi.Todo.Database
{
    public class Seeder
    {
        readonly AppDbContext _context;

        public Seeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            Console.WriteLine("Seeding...");

            if (!_context.Roles.Any())
            {
                var roles = new List<Role> {
                    new Role
                    {
                        Name = Roles.Admin
                    },
                    new Role
                    {
                        Name = Roles.User
                    }
                };

                _context.AddRange(roles);
                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                var admin = new User
                {
                    Email = "admin@gmail.com",
                    FirstName = "System",
                    LastName = "Admin"
                };

                _context.Users.Add(admin);
                _context.SaveChanges();

                var adminRole = _context.Roles.FirstOrDefault(x => x.Name == Roles.Admin);
                var userRole = _context.Roles.FirstOrDefault(x => x.Name == Roles.User);

                var userRoles = new List<UserRole>();
                if (adminRole != null && userRole != null)
                {
                    userRoles.AddRange(new List<UserRole>
                    {
                        new UserRole
                        {
                            RoleId = adminRole.Id,
                            UserId = admin.Id
                        },
                        new UserRole {
                            RoleId = userRole.Id,
                            UserId = admin.Id
                        }
                    });
                }
                _context.AddRange(userRoles);

                await _context.SaveChangesAsync();
            }
        }
    }
}

