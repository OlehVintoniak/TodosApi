using Microsoft.AspNetCore.Http;
using System.Linq;
using WebApi.Todo.Interfaces;

namespace WebApi.Todo.Auth
{
    public class UserWrapper : IUserWrapper
    {
        private readonly IHttpContextAccessor _accessor;

        public UserWrapper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public UserPrincipal CurrentUser => new UserPrincipal(_accessor.HttpContext.User);

        public long Id => long.Parse(
            CurrentUser.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
    }
}
