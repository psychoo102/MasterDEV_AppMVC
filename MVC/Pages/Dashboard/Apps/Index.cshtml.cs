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
    public class IndexModel : PageModel
    {
        private readonly MVC.Contexts.AppsContext _context;

        public IndexModel(MVC.Contexts.AppsContext context)
        {
            _context = context;
        }

        public IList<App> App { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Apps != null)
            {
                App = await _context.Apps.ToListAsync();
            }
        }
    }
}
