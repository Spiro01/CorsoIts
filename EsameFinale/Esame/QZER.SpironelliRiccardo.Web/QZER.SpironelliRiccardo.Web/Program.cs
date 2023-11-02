using EsameWebApp.Persistence;
using Microsoft.Azure.Devices;
using QZER.SpironelliRiccardo.Web.Interfaces;
using QZER.SpironelliRiccardo.Web.Models;
using QZER.SpironelliRiccardo.Web.Repository;
using QZER.SpironelliRiccardo.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IRoomRepository,RoomRepository>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IIotHubService,IotHubService>();
builder.Services.AddScoped(sp => ServiceClient.CreateFromConnectionString(builder.Configuration.GetConnectionString("IotHub")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
