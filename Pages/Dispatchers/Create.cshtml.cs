﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject.Data;
using HaulMaster.Models;

namespace HaulMaster.Pages.Dispatchers
{
    public class CreateModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public CreateModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Dispatcher Dispatcher { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Dispatcher == null || Dispatcher == null)
            {
                return Page();
            }

            _context.Dispatcher.Add(Dispatcher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
