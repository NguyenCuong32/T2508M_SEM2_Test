using Microsoft.EntityFrameworkCore;
using EXAM.Models;
using EXAM.Repository;
using EXAM.Service;

namespace EXAM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<ComicDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repository and Service layers
            builder.Services.AddScoped<IComicBookRepository, ComicBookRepository>();
            builder.Services.AddScoped<IComicBookService, ComicBookService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();
            builder.Services.AddScoped<IRentalService, RentalService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
                pattern: "{controller=ComicBooks}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
