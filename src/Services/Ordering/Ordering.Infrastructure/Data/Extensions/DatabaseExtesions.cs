using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabaseExtesions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);

        }
        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrderItemsAsync(context);

        }


        private static async Task SeedCustomerAsync(ApplicationDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(InitailData.Customers);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedProductAsync(ApplicationDbContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(InitailData.Products);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedOrderItemsAsync(ApplicationDbContext context)
        {
            if (!await context.OrderItems.AnyAsync())
            {
                await context.Orders.AddRangeAsync(InitailData.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
