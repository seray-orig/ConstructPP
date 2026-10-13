using ConstructPP.API.Services.Interfaces;

namespace ConstructPP.API.Data
{
    public static class CreateFirstAdminUserDB
    {
        public static void CreateFirstAdminUser(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var scopeProvider = scope.ServiceProvider;
                var db = scopeProvider.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                if (!db.Users.Any())
                {
                    var userService = scopeProvider.GetRequiredService<IUserService>();

                    var login = Guid.NewGuid().ToString();
                    var password = Guid.NewGuid().ToString();

                    userService.CreateUser(login, password);

                    WriteToFile(login, password);
                }
            }
        }

        private static void WriteToFile(string login, string password)
        {
            var text = $"Admin User\nLogin: {login}\nPassword: {password}";

            if (OperatingSystem.IsWindows())
            {
                Console.WriteLine(text);
                return;
            }

            var filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Construct++User.txt");

            File.WriteAllText(filePath, text);
        }
    }
}
