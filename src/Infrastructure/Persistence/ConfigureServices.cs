using System.Diagnostics.CodeAnalysis;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VsExample.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static void AddApplicationPersistence(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("ProductsDb");
            
            if (environment.IsDevelopment())
                options.EnableSensitiveDataLogging();
        });
        
        // Seed data after the database is created
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        using IServiceScope scope = serviceProvider.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Ensure the database is created
        context.Database.EnsureCreated();

        // Seed the data if needed
        SeedDatabase(context);
    }
    
    private static void SeedDatabase(ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            Faker faker = new Faker();
            
            for (int i = 1; i <= 1000; i++)
            {
                string productName = faker.Commerce.ProductName();
                string productDescription = $"{productName} is a wonderful product";
                
                context.Products.Add(new()
                {
                    Name = productName,
                    Price = decimal.Parse(faker.Commerce.Price()),
                    Description = productDescription,
                    CreatedAt = DateTime.Now
                });    
            }
            
            context.SaveChanges();
        }
    }
}