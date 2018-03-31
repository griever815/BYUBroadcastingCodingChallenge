using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CrazyIrasRestaurantHours.Models.SeedInfo
{
    public class SeedRestaurantData
    {
        public void SeedData(RestaurantContext context)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Models\\SeedRestaurantInfo\\rest_hours.json");

            List<SeedRestaurant> seedRestaurants = new List<SeedRestaurant>();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                seedRestaurants = JsonConvert.DeserializeObject<List<SeedRestaurant>>(json);
            }

            foreach (var seedRestaurant in seedRestaurants)
            {
                Restaurant restaurant = new Restaurant { Name = seedRestaurant.Name };
                context.Restaurant.Add(restaurant);

                ParseRestaurantTimes(seedRestaurant, context, restaurant);

            }
        }

        private static void ParseRestaurantTimes(SeedRestaurant seedRestaurant, RestaurantContext context, Restaurant restaurant)
        {
            var daysOfWeekEnumList = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

            foreach (var time in seedRestaurant.Times)
            {
                var daysOpen = new List<DayOfWeek>();

                var openingTime = new TimeSpan();
                var closingTime = new TimeSpan();
                var openingAMOrPM = "";
                var closingAMOrPM = "";

                var timeChunks = time.Split(' ');
                //Splitting the time by the ' ', it will format give a string[] that looks like "Mon-Thu,Sun,11:30,am,-,9,pm"
                //I then parse each timeChunk.

                int timeChunkCounter = 0;
                foreach (var timeChunk in timeChunks)
                {
                    timeChunkCounter++;

                    //If it contains a - and is longer than 1, it means it is a range of days
                    if (timeChunk.Contains("-") && timeChunk.Length > 1)
                    {
                        GetDaysFromRange(timeChunk, daysOfWeekEnumList, ref daysOpen);
                    }
                    //If it has a length of 3, it means it is a singular day of the week.
                    else if (timeChunk.Length == 3)
                    {
                        GetSingleDay(timeChunk, daysOfWeekEnumList, ref daysOpen);
                    }
                    //If it parses as a TimeSpan, it is either the opening or closing time.
                    else if (DateTime.TryParse(timeChunk, out DateTime dateTimeResult) || Int32.TryParse(timeChunk, out int intResult))
                    {
                        GetTimesOfDay(timeChunk, timeChunks, ref openingTime, ref closingTime, timeChunkCounter);
                    }
                    //If it contains am or pm, then it indicates time of day.
                    else if (timeChunk.Contains("am") || timeChunk.Contains("pm"))
                    {
                        GetAMOrPMs(timeChunk, timeChunks, ref openingAMOrPM, ref closingAMOrPM, timeChunkCounter);
                    }

                }

                AddAmOrPmToTimes(ref openingTime, ref closingTime, openingAMOrPM, closingAMOrPM);

                foreach (DayOfWeek dayOfWeek in daysOpen)
                {
                    RestaurantHasTime restaurantHasTime =
                        new RestaurantHasTime
                        {
                            StartTime = Convert.ToDateTime(openingTime.ToString()),
                            EndTime = Convert.ToDateTime(closingTime.ToString()),
                            DayOfWeekInt = (int)dayOfWeek,
                            DayOfWeekString = dayOfWeek.ToString(),
                            RestaurantID = restaurant.ID
                        };

                    if (restaurantHasTime.StartTime > restaurantHasTime.EndTime)
                    {
                        restaurantHasTime.EndTime = restaurantHasTime.EndTime.AddDays(1);
                    }
                    context.RestaurantHasTime.Add(restaurantHasTime);
                }


                var test = time;
            }
        }

        private static void AddAmOrPmToTimes(ref TimeSpan openingTime, ref TimeSpan closingTime, string openingAmorPm, string closingAmorPm)
        {
            var opening = openingTime + " " + openingAmorPm.ToUpper();
            var closing = closingTime + " " + closingAmorPm.ToUpper();

            openingTime = DateTime.Parse(opening).TimeOfDay;
            closingTime = DateTime.Parse(closing).TimeOfDay;

        }

        private static void GetAMOrPMs(string timeChunk, string[] timeChunks, ref string openingAmOrPm, ref string closingAmOrPm, int timeChunkCounter)
        {
            if (timeChunkCounter != timeChunks.Count())
            {
                openingAmOrPm = timeChunk;
            }
            else
            {
                closingAmOrPm = timeChunk;
            }
        }

        private static void GetTimesOfDay(string timeChunk, string[] timeChunks, ref TimeSpan openingTime, ref TimeSpan closingTime, int timeChunkCounter)
        {
            var timeOfDay = new TimeSpan();
            if (DateTime.TryParse(timeChunk, out DateTime dateTimeResult))
            {
                timeOfDay = DateTime.Parse(timeChunk).TimeOfDay;
            }
            else if (Int32.TryParse(timeChunk, out int intResult))
            {
                timeOfDay = TimeSpan.ParseExact(timeChunk, "%h", null);
            }

            var lastTimeSpecified = timeChunks.Count() -1;
            //If its not the last time specified, then it is the opening time.
            if (timeChunkCounter != lastTimeSpecified)
            {
                openingTime = timeOfDay;
            }
            else
            {
                closingTime = timeOfDay;
            }
        }

        private static void GetSingleDay(string timeChunk, IEnumerable<DayOfWeek> daysOfWeekEnumList, ref List<DayOfWeek> daysOpen)
        {
            var singleDayAbbreviation = timeChunk.Substring(0, 3);
            var singleDay = daysOfWeekEnumList.First(s => s.ToString().StartsWith(singleDayAbbreviation));
            daysOpen.Add(singleDay);
        }

        private static void GetDaysFromRange(string timeChunk, IEnumerable<DayOfWeek> daysOfWeekEnumList, ref List<DayOfWeek> daysOpen)
        {
            var firstDayAbbreviation = timeChunk.Substring(0, 3);
            var lastDayAbbreviation = timeChunk.Substring(4, 3);

            var firstDay = daysOfWeekEnumList.First(s => s.ToString().StartsWith(firstDayAbbreviation));
            var lastDay = daysOfWeekEnumList.First(s => s.ToString().StartsWith(lastDayAbbreviation));

            //If the last day is Sunday, the Enum is 0 and throws the for loop off. So I add it and set the last day to saturday.
            if (lastDay == DayOfWeek.Sunday)
            {
                var sunday = DayOfWeek.Sunday;
                daysOpen.Add(sunday);
                lastDay = DayOfWeek.Saturday;
            } 

            //Sun = 0. This for loop does nothing on cases where it ends with sunday.
            for (int i = (int)firstDay; i <= (int)lastDay; i++)
            {
                var currentDay = daysOfWeekEnumList.First(s => (int)s == i);
                daysOpen.Add(currentDay);
            }
        }

    }
}
