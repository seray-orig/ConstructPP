using Microsoft.Data.Sqlite;
Console.WriteLine("ok");
using var c = new SqliteConnection("Data Source=:memory:");
c.Open();
Console.WriteLine("opened OK");
