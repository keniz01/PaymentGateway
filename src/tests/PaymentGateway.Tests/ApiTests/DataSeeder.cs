using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Persistence;

namespace PaymentGateway.Tests;

public static class DataSeeder
{
    public static void Seed(IServiceCollection services)
    {
            var provider = services.BuildServiceProvider();

            using var serviceScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var fakeData = BogusDataGenerator.Generate();

            foreach (var command in fakeData)
            {
                context.Entry(command).State = EntityState.Added;
            }

            context.SaveChanges();
    } 
}