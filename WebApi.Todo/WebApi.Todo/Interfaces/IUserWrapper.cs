using WebApi.Todo.Auth;

namespace WebApi.Todo.Interfaces
{
    public interface IUserWrapper
    {
        UserPrincipal CurrentUser { get; }

        long Id { get; }
    }
}