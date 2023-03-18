using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly PostAppContext _context;

        public EditModel(PostAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

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
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post =  await _context.Posts.FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            Post = post;
           ViewData["AuthorId"] = new SelectList(_context.AppUsers, "UserId", "Email");
           ViewData["CategoryId"] = new SelectList(_context.PostCategories, "CategoryId", "CategoryId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.PostId))
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

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
