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
    public class EditModel : PageModel
    {
        private readonly PostAppContext _context;

        public EditModel(PostAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppUser AppUser { get; set; } = default!;


        public AppUser Account { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
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

            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var appuser = await _context.AppUsers.FirstOrDefaultAsync(m => m.UserId == id);
            if (appuser == null)
            {
                return NotFound();
            }
            AppUser = appuser;
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AppUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(AppUser.UserId))
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

        private bool AppUserExists(int id)
        {
          return (_context.AppUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
