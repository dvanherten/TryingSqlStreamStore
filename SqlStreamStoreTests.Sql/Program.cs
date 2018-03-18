using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SqlStreamStoreTests.Sql.Extensions;
using SqlStreamStoreTests.Sql.Infrastructure;

namespace SqlStreamStoreTests.Sql
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
