using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly PostAppContext _context;

        public CreateModel(PostAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AppUser AppUser { get; set; } = default!;

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
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AppUsers == null || AppUser == null)
            {
                return Page();
            }

            _context.AppUsers.Add(AppUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
