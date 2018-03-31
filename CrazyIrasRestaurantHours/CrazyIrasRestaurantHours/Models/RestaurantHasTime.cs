using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyIrasRestaurantHours.Models
{
    public class RestaurantHasTime
    {
        public int ID { get; set; }
        public int RestaurantID { get; set; }
        public int DayOfWeekInt { get; set; }
        public string DayOfWeekString { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


    }
}
