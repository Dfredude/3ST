﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;
using System.Text.Json;

namespace FinalProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FinalProjectContext _context;
        public readonly IWebHostEnvironment _env;
        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public string FeaturedProductImageURL { get; set; } = default!;
        [BindProperty]
        public List<Product> Products { get; set; } = default!;
        private String featuredProductName = "Perishable";

        public IndexModel(ILogger<IndexModel> logger, FinalProjectContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch first product on the DB
            var product = await _context.Product.FirstOrDefaultAsync(p => p.ProductName == featuredProductName);
            if (product != null)
            {
                Product = product;
                FeaturedProductImageURL = Path.Combine("images", Product.ImageName);
            } else
            {
                FeaturedProductImageURL = Path.Combine("images", "no-image.png");
            }

            // Fetch all products on the DB
            Products = await _context.Product.ToListAsync();

            // Get number of items in shopping cart
            var ShoppingCart = Request.Cookies["ShoppingCart"];
            ViewData["CartCount"] = 0;
            if (ShoppingCart != null)
            {
                List<int> items = JsonSerializer.Deserialize<List<int>>(ShoppingCart);
                ViewData["CartCount"] = items.Count;
            }

            // Redirect to the product page
            return Page();
        }
    }
}