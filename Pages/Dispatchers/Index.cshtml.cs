using System;
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
    public class IndexModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public IndexModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        public IList<Dispatcher> Dispatcher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Dispatcher != null)
            {
                Dispatcher = await _context.Dispatcher.ToListAsync();
            }
        }
    }
}
