using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<Auth>();

builder.Services.AddDbContext<DbQuanLyKhachSanContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("connecString"));
});
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
    pattern: "{controller=Home}/{action=Home}/{id?}");
    //pattern: "Admin/{controller=TaiKhoan}/{action=XacThuc}/{9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2}");

app.Run();
