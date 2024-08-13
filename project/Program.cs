using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project.Domain;
using project.Domain.Repositories.Abstract;
using project.Domain.Repositories.EntityFramework;
using project.Service;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

// Configure services


var configuration = builder.Configuration; // Retrieve the IConfiguration from the builder

// Your existing configuration setup (if any)
configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
configuration.AddEnvironmentVariables();
configuration.AddCommandLine(args);

services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
services.AddTransient<IPhotoFieldsReporitory, EFPhotoFieldsRepository>();
services.AddTransient<DataManager>();
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("AppDbConnectionString")),
	ServiceLifetime.Scoped // Устанавливаем время жизни службы как Scoped
);

// Configure identity system
services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// Configure authentication cookie
services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "zbk";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None; // Добавьте эту строку
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});

services.AddAuthorization(x =>
{
    x.AddPolicy("HortArea", policy => { policy.RequireRole("admin"); });
});
services.AddControllersWithViews(x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "HortArea"));
});
builder.Configuration.Bind("Project", new Config());
var app = builder.Build();

// Your existing application setup

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Apply migrations and create the database if it doesn't exist


// Connect authentication and authorization
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "hort",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
