using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject.Data;
using FinalProject.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Pages.Products
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(FinalProject.Data.FinalProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        [DisplayName("Upload Image")]
        public IFormFile ImageUpload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Product.ImageName = ImageUpload.FileName;
            if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }

            // Save Image on server
            DateTime now = DateTime.Now;
            string time = now.ToString("yyyyMMddHHmmssffff");
            string ImageFileName = time + "-" + Product.ImageName;
            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", ImageFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageUpload.CopyToAsync(stream);
            }
            Product.ImageName = ImageFileName;
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
