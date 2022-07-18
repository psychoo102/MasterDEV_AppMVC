using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.Contexts;
using MVC.Models;

namespace MVC.Pages.Dashboard.Apps
{
    public class DetailsModel : PageModel
    {
        private readonly MVC.Contexts.AppsContext _context;

        public DetailsModel(MVC.Contexts.AppsContext context)
        {
            _context = context;
        }

      public App App { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }

            var app = await _context.Apps.FirstOrDefaultAsync(m => m.id == id);
            if (app == null)
            {
                return NotFound();
            }
            else 
            {
                App = app;
            }
            return Page();
        }
    }
}
