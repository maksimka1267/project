using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using project.Domain;
using project.Domain.Repositories.Abstract;
using project.Domain.Repositories.EntityFramework;
using project.Service;

var builder = WebApplication.CreateBuilder(args);
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Retrieve the IConfiguration from the builder
var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
configuration.AddEnvironmentVariables();
configuration.AddCommandLine(args);

// Configure services
builder.Services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
builder.Services.AddTransient<DataManager>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("AppDbConnectionString")),
    ServiceLifetime.Scoped);

// Configure identity system
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Configure authentication cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "zbk";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Увеличиваем время до 30 минут
});


// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HortArea", policy => policy.RequireRole("admin"));
});

// Add caching services
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

// Register custom conventions
builder.Services.AddControllersWithViews(options =>
{
    options.Conventions.Add(new AdminAreaAuthorization("Hort", "HortArea"));
});

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure response compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enable for HTTPS
    options.Providers.Add<BrotliCompressionProvider>(); // Add Brotli compression
    options.Providers.Add<GzipCompressionProvider>(); // Add Gzip compression
    options.MimeTypes = new[] // Set MIME types for compression
    {
        "text/plain",
        "text/css",
        "application/javascript",
        "text/html",
        "application/xml",
        "text/xml",
        "application/json",
        "text/json",
        "image/svg+xml"
    };
});

// Configure Brotli and Gzip compression settings
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest; // Set Brotli compression level
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest; // Set Gzip compression level
});

// Configure HTTP/2 support
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable response caching
app.UseResponseCaching();

// Enable response compression
app.UseResponseCompression();

// Enable static file caching
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000");
    }
});

app.UseRouting();

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
