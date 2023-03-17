using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly SignalRAssignment.Models.PostAppContext _context;

        public IndexModel(SignalRAssignment.Models.PostAppContext context)
        {
            _context = context;
        }

        public IList<AppUser> AppUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AppUsers != null)
            {
                AppUser = await _context.AppUsers.ToListAsync();
            }
        }
    }
}
