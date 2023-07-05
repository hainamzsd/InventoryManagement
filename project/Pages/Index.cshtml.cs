using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.SessionExtensions;

namespace project.Pages
{

    public class IndexModel : PageModel
    {

        public IndexModel()
        {
            
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Username") != "admin")
            {
                return RedirectToPage("/Login");
            }

            // Continue with the page logic
            return Page();
        }
    }
}