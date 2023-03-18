using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages
{
    public class IndexModel : PageModel
    {

        private readonly PostAppContext _context;

        public IndexModel(PostAppContext context)
        {
            _context = context;
        }

        public AppUser Account { get; set; } = default;

        public async Task<IActionResult> OnGetAsync()
        {
            return Redirect("/Posts/Index");
            //return RedirectToRoute("./ProjectDetails", new { id = Submission.ID });

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

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("FullName");
            HttpContext.Session.Remove("UserId");
            return RedirectToPage("/Users/Login");
        }
    }
}