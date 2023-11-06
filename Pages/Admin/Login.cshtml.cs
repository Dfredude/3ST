using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (Username == "admin" && Password == "admin")
            {
                // Add auth cookie
                // Setup session
                var claims = new List<Claim>{
                new Claim(Password, "admin"),
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties());
                //Response.Cookies.Append("Admin", "true");
                //Response.Redirect("/Products/Index");
                return RedirectToPage("/Products/Index");
            }
            else
            {
                ModelState.AddModelError("LoginError", "Username or password is incorrect");
                return Page();
            }
        }
    }
}
