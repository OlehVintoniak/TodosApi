using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebApi.Todo.Models.Abstract;

namespace WebApi.Todo.Models
{
    public class Role : IdentityRole<long>, IEntity<long>
    {
        public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    }
}
