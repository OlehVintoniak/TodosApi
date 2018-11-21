using System.Security.Claims;
using System.Security.Principal;

namespace WebApi.Todo.Auth
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public UserPrincipal(IPrincipal principal) : base(principal) { }

        public long Id { get; set; }
    }
}
