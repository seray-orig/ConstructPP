using ConstructPP.API.Models;

namespace ConstructPP.API.Services.Interfaces
{
    public interface IUserService
    {
        public User? GetUserBy(string login, string password);
        public bool CreateUser(string login, string password);
    }
}
