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
    public class CreateModel : PageModel
    {
        private readonly PostAppContext _context;

        public CreateModel(PostAppContext context)
        {
            _context = context;
        }

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

            //ViewData["AuthorId"] = new SelectList(_context.AppUsers, "UserId", "Email");
            ViewData["CategoryId"] = new SelectList(_context.PostCategories, "CategoryId", "CategoryName");
            ViewData["PublishStatus"] = new SelectList(PostStatus.getSattus(), "PublishStatus", "StatusName");

            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            Post.CreatedDate = DateTime.Now;
            Post.UpdatedDate = DateTime.Now;
            Post.AuthorId = HttpContext.Session.GetInt32("UserId");
            Post.Author = Account;


            //DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //DateTime.Now.ToString("hh:mm:ss.fff tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //DateTime.Now.ToString("HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (!ModelState.IsValid || _context.Posts == null || Post == null)
            {
                return Page();
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
