using ElectronicQueue.Database.Initializers;
using Microsoft.EntityFrameworkCore;
using MvcApp.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connection));

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<DatabaseContext>();
    
    db.Database.Migrate();
    var statusInit = StatusInitializer.InitializeAsync(db);
    statusInit.Wait();
    var themesInit = TestThemeInitializer.InitializeAsync(db);
    themesInit.Wait();

}

app.Run();
