using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrazyIrasRestaurantHours.Models.SeedInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrazyIrasRestaurantHours.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RestaurantContext(
                serviceProvider.GetRequiredService<DbContextOptions<RestaurantContext>>()))
            {
                // Look for any Restaurants.
                if (!context.Restaurant.Any())
                {
                    var seedRestaurantData = new SeedRestaurantData();
                    seedRestaurantData.SeedData(context);
                }


                context.SaveChanges();
            }
        }
    }
}
