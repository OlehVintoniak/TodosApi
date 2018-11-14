using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Todo.Auth
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "http://localhost:4200/";
        const string Key = "mysupersecret_secretkey!123";
        public const int Lifetime = 14; // 14 days
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
