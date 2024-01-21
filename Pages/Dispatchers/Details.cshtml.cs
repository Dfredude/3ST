﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using HaulMaster.Models;

namespace HaulMaster.Pages.Dispatchers
{
    public class DetailsModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public DetailsModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

      public Dispatcher Dispatcher { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dispatcher == null)
            {
                return NotFound();
            }

            var dispatcher = await _context.Dispatcher.FirstOrDefaultAsync(m => m.ID == id);
            if (dispatcher == null)
            {
                return NotFound();
            }
            else 
            {
                Dispatcher = dispatcher;
            }
            return Page();
        }
    }
}
