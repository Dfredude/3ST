using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace HaulMaster.Pages
{
    public class ShoppingCartModel : PageModel
    {
        FinalProjectContext _db;
        [BindProperty]
        public List<Product> CartItems { get; set; } = new();
        private float PriceTotal { get; set; } = 0;
        public ShoppingCartModel(FinalProjectContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            // Get shopping cart cookie
            var ShoppingCart = Request.Cookies["ShoppingCart"];
            List<int> product_ids = default!;
            if (ShoppingCart != null)
            {
                product_ids = JsonSerializer.Deserialize<List<int>>(ShoppingCart);
                ViewData["CartCount"] = product_ids.Count;
            }
            else
            {
                ViewData["CartCount"] = 0;
            }
            // Get products from DB
            foreach (int prod_id in product_ids)
            {
                Product prod = _db.Product.FirstOrDefault(p => p.ID == prod_id);
                CartItems.Add(prod);
                PriceTotal += prod.Price;
            }
            ViewData["PriceTotal"] = PriceTotal;
        }

        public IActionResult OnGetRemoveFromCart(int id)
        {
            // Get shopping cart cookie
            var ShoppingCart = Request.Cookies["ShoppingCart"];
            List<int> product_ids = default!;
            if (ShoppingCart != null)
            {
                product_ids = JsonSerializer.Deserialize<List<int>>(ShoppingCart);
                ViewData["CartCount"] = product_ids.Count;
            }
            else
            {
                return Page();
            }
            // Remove item from cookie
            product_ids.Remove(id);
            // Update cookie
            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(product_ids));
            return RedirectToPage("ShoppingCart");
        }
    }
}
