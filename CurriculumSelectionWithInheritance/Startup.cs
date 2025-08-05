using CurriculumSelectionWithInheritance.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurriculumSelectionWithInheritance
{
    public class Startup
    {
        public Startup(IConfiguration iConfiguration)
        {
            Configuration = iConfiguration;
        }

        public IConfiguration Configuration { get; }

        #region snippet
        public void ConfigureServices(IServiceCollection iServiceCollection)
        {
            iServiceCollection.AddDbContext<CurriculumSelectionWithInheritanceDbContext>(delegate(DbContextOptionsBuilder dbContextOptionsBuilder) {
                dbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            iServiceCollection.AddDatabaseDeveloperPageExceptionFilter();

            iServiceCollection.AddControllersWithViews();
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder iApplicationBuilder, IWebHostEnvironment iWebHostEnvironment)
        {
            if (iWebHostEnvironment.IsDevelopment())
            {
                iApplicationBuilder.UseDeveloperExceptionPage();
            }
            else
            {
                iApplicationBuilder.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                iApplicationBuilder.UseHsts();
            }
            iApplicationBuilder.UseHttpsRedirection();
            iApplicationBuilder.UseStaticFiles();

            iApplicationBuilder.UseRouting();

            iApplicationBuilder.UseAuthorization();

            #region snippet2
            iApplicationBuilder.UseEndpoints(delegate(IEndpointRouteBuilder iEndpointRouteBuilder) 
            {
                iEndpointRouteBuilder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
        }
    }
}
