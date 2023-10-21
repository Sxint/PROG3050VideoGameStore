using Microsoft.EntityFrameworkCore;
using PROG3050VideoGameStore.Models;
using AspNetCore.ReCaptcha;

var builder = WebApplication.CreateBuilder(args);

//
var connStr = builder.Configuration.GetConnectionString("GamesDb");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connStr));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddReCaptcha(builder.Configuration.GetSection("GoogleReCaptcha"));

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
