using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.ResponseCompression;
using VsExample.Application;
using VsExample.Infrastructure.Logging;
using VsExample.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogging(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();
builder.Services.AddApplicationPersistence(builder.Environment);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }