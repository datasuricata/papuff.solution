﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using papuff.datainfra.ORM;
using papuff.datainfra.Seeder;
using System;
using System.Threading.Tasks;

namespace papuff.webapi {
    public class Program {
        public static void Main(string[] args) {

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.Migrate();

                    Task.Run(async () => {
                        await DataSeed.InitializeAsync(services);
                    }).Wait();

                } catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
