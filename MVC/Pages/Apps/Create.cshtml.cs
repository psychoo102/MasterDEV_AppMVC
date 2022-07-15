using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Contexts;
using MVC.Models;

namespace MVC.Pages.Apps
{
    public class CreateModel : PageModel
    {
        private readonly MVC.Contexts.AppsContext _context;

        public CreateModel(MVC.Contexts.AppsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public App App { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Apps == null || App == null)
            {
                return Page();
            }

            App.createdAt = DateTime.UtcNow;
            _context.Apps.Add(App);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
