using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class LoginModel : PageModel
    {


        public void OnGet()
        {
        }
        public IActionResult OnPost(string username, string password)
        {
            if (IsValidCredentials(username, password))
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToPage("/Index");
            }

            TempData["ErrorMessage"] = "Invalid username or password.";
            return Page();
        }

        private bool IsValidCredentials(string username, string password)
        {
            return (username == "admin" && password == "admin");
        }

        public IActionResult OnGetLogout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the login page
            return RedirectToPage("/Login");
        }
    }
}
