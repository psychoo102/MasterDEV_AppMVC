using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.Contexts;
using MVC.Models;

namespace MVC.Pages.Apps
{
    public class DeleteModel : PageModel
    {
        private readonly MVC.Contexts.AppsContext _context;

        public DeleteModel(MVC.Contexts.AppsContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }
            var app = await _context.Apps.FindAsync(id);

            if (app != null)
            {
                App = app;
                _context.Apps.Remove(App);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
