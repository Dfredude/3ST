using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;


namespace HaulMaster.Pages
{
    public class Order
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; } 
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; } 
        public string CCNumber { get; set; }
        public string CCExpiryDate { get; set; }
        public string CVV { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public string ProductsString { get; set; }

        public string GetJSONPayload()
        {
            var payload = new JsonObject
            {
                { "name", Name },
                { "address", Address },
                { "city", City },
                { "province", Province },
                { "postalCode", PostalCode },
                { "country", Country },
                { "ccNumber", CCNumber },
                { "ccExpiryDate", CCExpiryDate },
                { "cvv", CVV },
                { "products", ProductsString }
            };
            return payload.ToString();
        }
    }
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; } = new Order();

        private const string API_URL = "https://nscc-inet2005-purchase-api.azurewebsites.net/purchase";
        private FinalProjectContext _db;
        private readonly HttpClient HttpClient;

        public CheckoutModel(FinalProjectContext db, HttpClient httpClient)
        {
            _db = db;
            HttpClient = httpClient;
        }
        public async Task<IActionResult> OnGet(String products)
        {
            Console.WriteLine(products);
            // Assume the user is a guest for now
            // Get products from DB
            Order.ProductsString = products;
            List<int> product_IDs = DecodeCartItemsQueryString(products);
            foreach (int product_ID in product_IDs)
            {
                Product product = await _db.Product.FindAsync(product_ID);
                Order.Products.Add(product);
            }
            ViewData["CartCount"] = Order.Products.Count;
            return Page();
        }


        public List<int> DecodeCartItemsQueryString(String CartItemsQueryString)
        {
            String[] products_IDs_strings = CartItemsQueryString.Split(",");
            List<int> product_IDs = new List<int>();
            foreach (String product_ID_string in products_IDs_strings)
            {
                product_IDs.Add(Int32.Parse(product_ID_string));
            }
            return product_IDs;
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // When the customer selects the “Place Order” button, collect the input from the user
            // and “POST” the order data to the Purchase Processing API
            // Sample JSON
            //{
            //    "name": "string",
            //    "address": "string",
            //    "city": "string",
            //    "province": "string",
            //    "postalCode": "string",
            //    "country": "string",
            //    "ccNumber": number,
            //    "ccExpiryDate": "string",
            //    "cvv": number,
            //    "products": "string"
            //}   

            // Send the order object to the API

            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Create the JSON object
            //JsonObject payload = new JsonObject
            var payload = Order.GetJSONPayload();
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(API_URL, content);

            var responseString = await response.Content.ReadAsStringAsync();

            // If the API returns an error, display the error message to the user

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ModelState.AddModelError("", responseString);
                return Page();
            }



            // Clear cookie permanently "ShoppingCart"
            if (Request.Cookies["ShoppingCart"] != null)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Append("ShoppingCart", "", cookieOptions);
            }


            // Redirect to the order confirmation page

            return RedirectToPage("/OrderConfirmation", new { ConfirmationNumber = responseString.Trim('"') });

        }
    }
}
