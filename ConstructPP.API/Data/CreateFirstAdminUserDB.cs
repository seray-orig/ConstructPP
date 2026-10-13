using ConstructPP.API.Services.Interfaces;

namespace ConstructPP.API.Data
{
    public static class CreateFirstAdminUserDB
    {
        public static void CreateFirstAdminUser(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = services.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                if (!db.Users.Any())
                {
                    var userService = services.GetRequiredService<IUserService>();

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
                Console.WriteLine(text);
            else
                File.WriteAllText("~/Construct++User.txt", text);
        }
    }
}
