using ConstructPP.API.Data;
using ConstructPP.API.Services.Interfaces;

namespace ConstructPP.API.Program
{
    public static class CreateFirstAdminUserDB
    {
        public static void Create(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                if (!db.Users.Any())
                {
                    var userService = services.GetRequiredService<IUserService>();
                    var passwordHasher = services.GetRequiredService<IPasswordHasher>();

                    var login = Guid.NewGuid().ToString();
                    var password = Guid.NewGuid().ToString();
                    
                    userService.CreateUser(login, password);

                    var text = $"Admin User\nLogin: {login}\nPassword: {password}";

                    //TODO: Эту хуйню переписать по хорошему, просто чтобы у меня на компе не создавался текстовый файл.
                    if (OperatingSystem.IsWindows())
                        Console.WriteLine(text);
                    else
                        File.WriteAllText("/root/gmod-panel-user.txt", text);
                }
            }
        }
    }
}
