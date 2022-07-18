using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Contexts;
using MVC.Models;
using MVC.Models.Validations;

namespace MVC.Pages.Dashboard.Apps
{
    public class CreateModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly MVC.Contexts.AppsContext _context;

        public CreateModel(IWebHostEnvironment environment, MVC.Contexts.AppsContext context)
        {
            _environment = environment;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public App App { get; set; } = default!;

        [Required]
        [IsZIPFile]
        [BindProperty]
        public IFormFile ProjectFiles { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Apps == null || App == null)
            {
                return Page();
            }

            App.createdAt = DateTime.UtcNow;
            App.updatedAt = DateTime.UtcNow;
            _context.Apps.Add(App);
            await _context.SaveChangesAsync();

            string fileName = String.Format("{0}_PROJECT.zip", App.id);
            var file = Path.Combine(_environment.ContentRootPath, "uploads", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(file));
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await ProjectFiles.CopyToAsync(fileStream);
            }

            return RedirectToPage("./Index");
        }
    }
}
