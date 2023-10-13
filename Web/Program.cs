using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Web.Data;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connectionstring")));

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=GirisYap}/{id?}");

app.Run();
