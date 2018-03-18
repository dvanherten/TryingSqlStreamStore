using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SqlStreamStoreTests.Sql.Extensions
{
    public static class MigrateDatabaseExtension
    {
        public static IApplicationBuilder MigrateDbContext<TContext>(this IApplicationBuilder appBuilder) where TContext : DbContext
        {
            using (var scope = appBuilder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
                    context.Database.Migrate();
                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return appBuilder;
        }
    }
}
