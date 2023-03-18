using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly PostAppContext _context;

        public IndexModel(PostAppContext context)
        {
            _context = context;
        }

        public IList<PostCategory> PostCategory { get;set; } = default!;
        public AppUser Account { get; set; } = default;

        public async Task<IActionResult> OnGetAsync()
        {
            int? curr_acc_id = HttpContext.Session.GetInt32("UserId");
            if (curr_acc_id == null)
            {
                return Redirect("/Users/Login");
            }

            Account = await _context.AppUsers.FirstOrDefaultAsync(m => m.UserId.Equals(curr_acc_id));
            if (Account == null)
            {
                return NotFound();
            }
            if (_context.PostCategories != null)
            {
                PostCategory = await _context.PostCategories.ToListAsync();
            }
            return Page();
        
        }
    }
}
