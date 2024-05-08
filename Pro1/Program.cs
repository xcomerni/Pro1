using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pro1.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Pro1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Pro1Context") ?? throw new InvalidOperationException("Connection string 'Pro1Context' not found.")));

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust session timeout
    options.Cookie.HttpOnly = true; // Secure session cookie
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); // Enable session handling
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
