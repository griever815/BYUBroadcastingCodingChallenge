using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyIrasRestaurantHours.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<RestaurantHasTime> RestaurantHasTimes { get; set; }
    }
}
