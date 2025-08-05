using CurriculumSelectionWithInheritance.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CurriculumSelectionWithInheritance
{
    public class Program
    {
        public static void Main(String[] args)
        {
            IHost iHost = CreateHostBuilder(args).Build();

            Program.CreateDbIfNotExists(iHost);

            iHost.Run();
        }

        private static void CreateDbIfNotExists(IHost iHost)
        {
            using (IServiceScope iServiceScope = iHost.Services.CreateScope())
            {
                IServiceProvider iServiceProvider = iServiceScope.ServiceProvider;
                try
                {
                    CurriculumSelectionWithInheritanceDbContext curriculumSelectionWithInheritanceDbContext = iServiceProvider.GetRequiredService<CurriculumSelectionWithInheritanceDbContext>();
                    DbInitializer.Initialize(curriculumSelectionWithInheritanceDbContext);
                }
                catch (Exception exception)
                {
                    ILogger iLogger = iServiceProvider.GetRequiredService<ILogger<Program>>();
                    iLogger.LogError(exception, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(String[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(delegate (IWebHostBuilder iWebHostBuilder)
                {
                    iWebHostBuilder.UseStartup<Startup>();
                });
        }
    }
}