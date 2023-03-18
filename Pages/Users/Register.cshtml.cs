using SignalRAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asm02Solution.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly PostAppContext _context;

        public RegisterModel(PostAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AppUser Account { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AppUsers.Add(Account);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return RedirectToPage("/Users/Login");
            }
            return RedirectToPage("/Index");
        }
    }
}
