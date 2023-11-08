using BilgeShop.Business.Managers;
using BilgeShop.Business.Services;
using BilgeShop.Data.Context;
using BilgeShop.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductService, ProductManager>();

var connectionString = builder.Configuration.GetConnectionString("HomeConnection");

builder.Services.AddDbContext<BilgeShopContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
// IRepository tipinde bir newleme yap�ld���nda (Dependency Injection ile) - SqlRepository Kopyas� olu�turur.
// AddScoped -> Her istek i�in yeni bir kopya.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");
});


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();


// Area Route - default route'un �ncesinde yaz�lmal�.
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{Action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"
    );

app.Run();
