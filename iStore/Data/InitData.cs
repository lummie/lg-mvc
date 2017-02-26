using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace iStore.Models
{
    /**
    Show use of a static class and method to initialize the data in the database if none exists.
     */
    public static class InitData
    {

        public static void InitialiseStore(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext())
            {
                // check if there is already data as we dont want to initialise it twice
                if (!context.StoreItems.AnyAsync().Result)
                {
                    // add some products
                    context.StoreItems.AddRange(
                         new StoreItem
                         {
                             ID = Guid.NewGuid(),
                             Name = "Laptop",
                             AddedDate = DateTime.Parse("2017-1-17 12:01:01"),
                             Price = 999.99M
                         },
                         new StoreItem
                         {
                             ID = Guid.NewGuid(),
                             Name = "Desktop",
                             AddedDate = DateTime.Parse("2017-1-17 12:01:01"),
                             Price = 499.99M
                         },
                         new StoreItem
                         {
                             ID = Guid.NewGuid(),
                             Name = "Mouse",
                             AddedDate = DateTime.Parse("2017-1-17 12:01:01"),
                             Price = 7.99M
                         }
                    );
                    context.SaveChanges();
                }
            }

        }

    }
}