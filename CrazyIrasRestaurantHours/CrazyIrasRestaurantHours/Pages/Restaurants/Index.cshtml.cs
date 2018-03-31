using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrazyIrasRestaurantHours.Models;
using Itenso.TimePeriod;

namespace CrazyIrasRestaurantHours.Pages.Restaurants
{
    public class IndexModel : PageModel
    {
        private readonly CrazyIrasRestaurantHours.Models.RestaurantContext _context;

        public IndexModel(CrazyIrasRestaurantHours.Models.RestaurantContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurants { get; set; }
        public IList<RestaurantHasTime> RestaurantHasTimes { get; set; }

        public async Task OnGetAsync(string searchDate, string searchTime)
        {
            var restaurants = from restaurant in _context.Restaurant
                              select restaurant;

            restaurants = FilterRestaurants(searchDate, searchTime, restaurants);



            Restaurants = await restaurants.ToListAsync();
            RestaurantHasTimes = await _context.RestaurantHasTime.ToListAsync();
        }

        private IQueryable<Restaurant> FilterRestaurants(string searchDate, string searchTime, IQueryable<Restaurant> restaurants)
        {
            if (!string.IsNullOrEmpty(searchDate) || !string.IsNullOrEmpty(searchTime))
            {
                var daysOfWeekEnumList = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

                DateTime? selectedDayOfWeek = null;
                TimeSpan? selectedTime = null;
                DateTime selectedDateTime = DateTime.Today;


                if (!string.IsNullOrEmpty(searchDate))
                {
                    selectedDayOfWeek = DateTime.Parse(searchDate);
                }

                if (!string.IsNullOrEmpty(searchTime))
                {
                    selectedTime = TimeSpan.Parse(searchTime);
                    var rht = _context.RestaurantHasTime.FirstOrDefault();
                    var date = rht.StartTime;

                    selectedDateTime = new DateTime(date.Year, date.Month, date.Day, selectedTime.Value.Hours, selectedTime.Value.Minutes, selectedTime.Value.Seconds);
                }



                restaurants = from restaurant in restaurants
                              join restaurantHasTime in _context.RestaurantHasTime
                                  on restaurant.ID equals restaurantHasTime.RestaurantID

                              //StartTime Calculations
                              let startTime = restaurantHasTime.StartTime

                              ////EndTime Calculations
                              let endTime = restaurantHasTime.EndTime

                              ////Selected Time Calculations
                              let chosenTime = selectedDateTime
                              let modifiedChosenTime =  chosenTime >= startTime ?  chosenTime :chosenTime.AddDays(1)

                              where (selectedDayOfWeek == null || restaurantHasTime.DayOfWeekInt == (int)selectedDayOfWeek.Value.DayOfWeek)
                                    && (selectedTime == null || (startTime <= modifiedChosenTime && modifiedChosenTime <= endTime))

                              select restaurant;
            }

            restaurants = restaurants.OrderBy(s => s.ID).Distinct();
            return restaurants;
        }
    }
}
