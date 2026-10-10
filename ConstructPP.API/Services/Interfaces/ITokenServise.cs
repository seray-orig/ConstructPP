using ConstructPP.API.Models;

namespace ConstructPP.API.Services.Interfaces
{
    public interface ITokenServise
    {
        string GenerateToken(User user, bool rememberMe);
    }
}
