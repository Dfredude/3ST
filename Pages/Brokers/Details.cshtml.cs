using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using HaulMaster.Models;

namespace HaulMaster.Pages.Brokers
{
    public class DetailsModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public DetailsModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

      public Broker Broker { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Broker == null)
            {
                return NotFound();
            }

            var broker = await _context.Broker.FirstOrDefaultAsync(m => m.ID == id);
            if (broker == null)
            {
                return NotFound();
            }
            else 
            {
                Broker = broker;
            }
            return Page();
        }
    }
}
