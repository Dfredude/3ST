using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using HaulMaster.Models;

namespace HaulMaster.Pages.Drivers
{
    public class DetailsModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public DetailsModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

      public Driver Driver { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver.FirstOrDefaultAsync(m => m.ID == id);
            if (driver == null)
            {
                return NotFound();
            }
            else 
            {
                Driver = driver;
            }
            return Page();
        }
    }
}
