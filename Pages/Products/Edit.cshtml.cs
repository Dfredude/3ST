using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using System.ComponentModel;

namespace FinalProject.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(FinalProject.Data.FinalProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty] // This is the image file uploaded to the server which is optional
        [DisplayName("Upload a new Image to update it")]
        public IFormFile? ImageUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product =  await _context.Product.FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ImageUpload != null)
            {
                Product.ImageName = ImageUpload.FileName ?? Product.ImageName;
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
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
