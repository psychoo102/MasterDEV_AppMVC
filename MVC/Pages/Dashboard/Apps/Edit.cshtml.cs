using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Models.Validations;

namespace MVC.Pages.Dashboard.Apps
{
    public class EditModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly MVC.Contexts.AppsContext _context;

        public EditModel(IWebHostEnvironment environment, MVC.Contexts.AppsContext context)
        {
            _environment = environment;
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

            var app =  await _context.Apps.FirstOrDefaultAsync(m => m.id == id);
            if (app == null)
            {
                return NotFound();
            }
            App = app;
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

            _context.Attach(App).State = EntityState.Modified;
            App.updatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppExists(App.id))
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

        private bool AppExists(int id)
        {
          return (_context.Apps?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
