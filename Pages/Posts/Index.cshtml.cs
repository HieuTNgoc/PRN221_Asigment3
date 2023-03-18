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
    public class IndexModel : PageModel
    {
        private readonly PostAppContext _context;

        public IndexModel(PostAppContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get; set; } = default!;
        public AppUser Account { get; set; } = default;
        public PostStatus Status { get; set; } = default;

        [BindProperty]
        public Post PostCreate { get; set; } = new Post();

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCategory { get; set; }
        public SelectList? Categories { get; set; }


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
            if (_context.Posts != null)
            {
                var post = from m in _context.Posts select m;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    post = post.Where(p => (p.Title.Contains(SearchString)) || (p.Content.Contains(SearchString)));
                }

                if (SelectedCategory != 0 && SelectedCategory != null)
                {
                    post = post.Where(p => p.CategoryId == SelectedCategory);
                }
                Post = await post.Where(p => p.PublishStatus == 1)
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .OrderByDescending(x => x.PostId)
                    .ToListAsync();
                Categories = new SelectList(_context.PostCategories, "CategoryId", "CategoryName");
            }
            PostCreate.CreatedDate = DateTime.Now;
            PostCreate.UpdatedDate = DateTime.Now;
            PostCreate.AuthorId = HttpContext.Session.GetInt32("UserId");
            PostCreate.Author = Account;
            ViewData["CategoryId"] = new SelectList(_context.PostCategories, "CategoryId", "CategoryName");
            ViewData["PublishStatus"] = new SelectList(PostStatus.getSattus(), "PublishStatus", "StatusName");
            return Page();
        }

    }
}
