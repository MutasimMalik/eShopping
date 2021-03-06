using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext,
                                           ILoggerFactory loggerFactory,
                                           int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                orderContext.Database.Migrate();

                if(orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreConfigureOrders());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                if(retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(ex.Message);
                    System.Threading.Thread.Sleep(2000);
                    await SeedAsync(orderContext, loggerFactory, retryForAvailability);
                }
            }
        }

        private static IEnumerable<Order> GetPreConfigureOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "qmm", FirstName = "Mutasim", LastName = "Malik", EmailAddress = "malik@gmail.com", AddressLine = "Dhaka", Country = "Bangladesh" }
            };
        }
    }
}
