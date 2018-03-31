using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CrazyIrasRestaurantHours.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<RestaurantHasTime> RestaurantHasTime { get; set; }

    }
}
