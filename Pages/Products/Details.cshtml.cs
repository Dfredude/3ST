using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using System.Text.Json;

namespace FinalProject.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;


        public DetailsModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var ShoppingCart = Request.Cookies["ShoppingCart"];
            if (ShoppingCart == null)
            {
                ViewData["CartCount"] = 0;
            }
            else
            {
                ViewData["CartCount"] = JsonSerializer.Deserialize<List<int>>(ShoppingCart).Count;
            }

            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }
            if (Product.ID == 0)
            {
                return NotFound();
            }

            // Get the cookie or create a new cookie if does not exist.
            // The cookie will have a 1 day expiry(24hrs).
            CookieOptions opt = new();

            opt.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(1));

            var ShoppingCart = Request.Cookies["ShoppingCart"];
            if (ShoppingCart == null)
            {
                List<int> cookies = new List<int>();
                cookies.Add(Product.ID);
                ViewData["CartCount"] = 1;
                string data = JsonSerializer.Serialize(cookies);
                Response.Cookies.Append("ShoppingCart", data, opt);
            }
            else
            {
                List<int> cookies = JsonSerializer.Deserialize<List<int>>(ShoppingCart);
                cookies.Add(Product.ID);
                ViewData["CartCount"] = cookies.Count;
                Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(cookies), opt);
            }
            return RedirectToPage("/Index");
        }

    }
}
