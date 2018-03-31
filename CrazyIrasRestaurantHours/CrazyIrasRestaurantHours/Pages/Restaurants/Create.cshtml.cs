using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrazyIrasRestaurantHours.Models;

namespace CrazyIrasRestaurantHours.Pages.Restaurants
{
    public class CreateModel : PageModel
    {
        private readonly CrazyIrasRestaurantHours.Models.RestaurantContext _context;

        public CreateModel(CrazyIrasRestaurantHours.Models.RestaurantContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Restaurant.Add(Restaurant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}