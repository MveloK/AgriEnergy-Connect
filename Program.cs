using EAPD7111_Agri_Energy_Connect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // <- Must be before UseAuthorization
app.UseAuthorization();

// Routing setup
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "dashboardemp",
    pattern: "{controller=Employee}/{action=Dashboard}/{id?}");

app.MapControllerRoute(
    name: "dashboardfarm",
    pattern: "{controller=Farmer}/{action=Dashboardfarm}/{id?}");

app.MapControllerRoute(
    name: "farmprod",
    pattern: "{controller=Employee}/{action=FarmerProducts}/{id?}");

app.MapControllerRoute(
    name: "farmview",
    pattern: "{controller=Farmer}/{action=MyProducts}/{id?}");


app.Run();
