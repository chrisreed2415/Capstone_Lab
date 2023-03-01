using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reed_Lab1.Pages.DB;
using System.ComponentModel.DataAnnotations;

namespace Reed_Lab1.Pages.Login
{
    public class SignupModel : PageModel
    { 
        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }
        [BindProperty]
        [Required]
        public string SelectedUser { get; set; }
        [BindProperty]
        [Required]
        public string Fullname { get; set; }
        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        public string Telephone { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("newsu", SelectedUser);
            HttpContext.Session.SetString("fullname", Fullname);
            HttpContext.Session.SetString("email", Email);
            HttpContext.Session.SetString("phone", Telephone);
            DBClass.CreateHashedUser(Username, Password, Fullname, Email, SelectedUser);
            DBClass.Lab3DBConnection.Close();
            DBClass.CreateUser(Fullname, Telephone, Email, Username);
            DBClass.Lab3DBConnection.Close();

            ViewData["UserCreate"] = "User Successfully Created!";

            return RedirectToPage("Login");
        }

    }
}
