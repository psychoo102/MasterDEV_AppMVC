using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Contexts;
using MVC.Models;
using MVC.Models.Validations;

namespace MVC.Pages.Dashboard.Apps
{
    public class UpdateModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly MVC.Contexts.AppsContext _context;

        public UpdateModel(IWebHostEnvironment environment, MVC.Contexts.AppsContext context)
        {
            _environment = environment;
            _context = context;
        }

        public App App { get; set; } = default!;

        [Required]
        [BindProperty]
        public int AppId { get; set; } = default!;

        [Required]
        [IsSemanticVersion]
        [BindProperty]
        public string AppVersion { get; set; } = default!;

        [Required]
        [IsZIPFile]
        [BindProperty]
        public IFormFile ProjectFiles { get; set; } = default!;

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

            AppId = app.id;
            AppVersion = app.version;
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

            try
            {
                App = await _context.Apps.FirstOrDefaultAsync(m => m.id == AppId);

                App.version = AppVersion;
                App.updatedAt = DateTime.UtcNow;

                _context.Attach(App).State = EntityState.Modified;
                string fileName = String.Format("{0}_PROJECT.zip", App.id);
                var file = Path.Combine(_environment.ContentRootPath, "uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(file));
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await ProjectFiles.CopyToAsync(fileStream);
                }
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
