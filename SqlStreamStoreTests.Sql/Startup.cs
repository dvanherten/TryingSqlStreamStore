using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SqlStreamStore;
using SqlStreamStore.Streams;
using SqlStreamStoreTests.Sql.Controllers;
using SqlStreamStoreTests.Sql.Extensions;
using SqlStreamStoreTests.Sql.Infrastructure;
using SqlStreamStoreTests.Sql.Models;

namespace SqlStreamStoreTests.Sql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReadContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            var settings = new MsSqlStreamStoreSettings(Configuration.GetConnectionString("DefaultConnection"));
            settings.Schema = "ES";
            services.AddSingleton(settings);
            services.AddSingleton<IStreamStore, MsSqlStreamStore>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStreamStore store, IServiceProvider serviceProvider)
        {
            app.MigrateDbContext<ReadContext>();

            store.SubscribeToAll(Position.None, async (subscription, message) =>
            {
                try
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ReadContext>();
                        var data = await message.GetJsonData();
                        var command = JsonConvert.DeserializeObject<Command>(data);
                        var readModel = context.ReadModels.Find(command.Id);
                        if (readModel == null)
                            context.Add(new ReadModel(command.Value, message.CreatedUtc));
                        else
                            readModel.UpdateValue(command.Value, message.CreatedUtc);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            if (store is MsSqlStreamStore msStore)
                msStore.CreateSchema().GetAwaiter().GetResult();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
