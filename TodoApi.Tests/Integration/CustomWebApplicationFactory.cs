using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data;
using System.Linq;

namespace TodoApi.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove all existing DbContext registrations
            var descriptorsToRemove = services
                .Where(d => d.ServiceType.FullName != null &&
                            d.ServiceType.FullName.Contains("DbContextOptions"))
                .ToList();

            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }

            // Register InMemory DB for testing
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Ensure the InMemory DB is created
            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
            db.Database.EnsureCreated();
        });
    }
}
