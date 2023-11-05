using Microsoft.EntityFrameworkCore;
using TicketTracker.Infrastructure.DataBaseContext;
using TicketTracker.Infrastructure.Extensions;
using TicketTracker.Infrastructure.Seeders;
using TicketTracker.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

#region Seeder - to delete on prod !

var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<TicketTrackerSeeder>(); 

await seeder.Seed(); //inserts first records to DB 

#endregion 


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
