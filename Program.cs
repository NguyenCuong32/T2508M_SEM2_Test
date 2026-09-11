using Microsoft.EntityFrameworkCore;
using T2508M_SEM_Test.Data;
using T2508M_SEM_Test.Repositories.Implementations;
using T2508M_SEM_Test.Repositories.Interfaces;
using T2508M_SEM_Test.Services.Implementations;
using T2508M_SEM_Test.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ComicSystemContext>(options =>
    options.UseMySQL(connectionString!));

builder.Services.AddScoped<IComicBookRepository, ComicBookRepository>();
builder.Services.AddScoped<IComicBookService, ComicBookService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();