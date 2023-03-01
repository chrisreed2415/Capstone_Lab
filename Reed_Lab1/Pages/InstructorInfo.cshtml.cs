using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reed_Lab1.Pages
{
    public class InstructorInfoModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("type") == "student" || HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/Access");
            }
            return Page();
        }
    }
}
