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
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public IndexModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                Product = await _context.Product.ToListAsync();
            }

            // Get number of items in shopping cart
            var ShoppingCart = Request.Cookies["ShoppingCart"];
            if (ShoppingCart != null)
            {
                List<int> items = JsonSerializer.Deserialize<List<int>>(ShoppingCart);
                ViewData["CartCount"] = items.Count;
            }
            else
            {
                ViewData["CartCount"] = 0;
            }
        }
    }
}
