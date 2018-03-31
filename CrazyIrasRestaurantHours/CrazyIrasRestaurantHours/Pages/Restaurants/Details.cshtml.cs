using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrazyIrasRestaurantHours.Models;

namespace CrazyIrasRestaurantHours.Pages.Restaurants
{
    public class DetailsModel : PageModel
    {
        private readonly CrazyIrasRestaurantHours.Models.RestaurantContext _context;

        public DetailsModel(CrazyIrasRestaurantHours.Models.RestaurantContext context)
        {
            _context = context;
        }

        public Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant = await _context.Restaurant.SingleOrDefaultAsync(m => m.ID == id);

            if (Restaurant == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
