using ConstructPP.API.Data;
using ConstructPP.API.Models;
using ConstructPP.API.Services.Interfaces;

namespace ConstructPP.API.Services
{
    public class UserService(AppDbContext context, IPasswordHasher passwordHasher) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public User? GetUserBy(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            User? user = _context.Users.FirstOrDefault(u => u.Login == login);

            if (user == null)
                return null;

            if (_passwordHasher.VerifyPassword(password, user.Password))
                return user;
            else
                return null;
        }

        public bool CreateUser(string login, string password)
        {
            if (GetUserBy(login, password) == null)
                return false;

            var user = new User
            {
                Login = login,
                Password = _passwordHasher.HashPassword(password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }
    }
}
