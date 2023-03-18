using SignalRAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SignalRAssignment.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly PostAppContext _context;


        public LoginModel(PostAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AppUser LoginAccount { get; set; }
        public AppUser Account { get; set; } = default!;



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Account = _context.AppUsers.FirstOrDefault(u => u.Email.Equals(LoginAccount.Email) && u.Password.Equals(LoginAccount.Password));
            if (Account != null)
            {
                HttpContext.Session.SetString("Email", Account.Email);
                HttpContext.Session.SetString("FullName", Account.FullName);
                HttpContext.Session.SetString("Address", Account.Addresss);
                HttpContext.Session.SetInt32("UserId", Account.UserId);
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("LoginAccount.Password", "Incorrect Email & Pasword!");
                return Page();
            }
        }

    }
}
