using ACMF_Final.Application.Interfaces;
using ACMF_Final.Application.Services;
using ACMF_Final.Domain.Interfaces;
using ACMF_Final.Infrastructure.Data;
using ACMF_Final.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace ACMF_Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure Razor to find Views in Web/Views/ folder
            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml");
            });

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Unit of Work and Repository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // Register Application Services
            builder.Services.AddScoped<IComicBookService, ComicBookService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<IReportService, ReportService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
