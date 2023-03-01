using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reed_Lab1.Pages.DataClasses;
using Reed_Lab1.Pages.DB;
using System.Data.SqlClient;

namespace Reed_Lab1.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string SelectedUser { get; set; }

        public void OnGet(String logout)
        {
            if (logout != null)
            {
                HttpContext.Session.Clear();
                ViewData["LoginMessage"] = "Log out successful";
            }
        }
        public IActionResult OnPost()
        {
            //string loginQuery = "SELECT COUNT(*) FROM Credentials where Username = '";
            //loginQuery += Username + "' and Password = '" + Password + "' and UserType = '" + SelectedUser + "'";

            //if (DBClass.LoginQuery(loginQuery) > 0)
            if (DBClass.HashedParameterLogin(Username, Password, SelectedUser))
            {
                HttpContext.Session.SetString("username", Username);
                HttpContext.Session.SetString("type", SelectedUser);
                DBClass.Lab3DBConnection.Close();
                return RedirectToPage("/Index");

            }
            else
            {
                ViewData["LoginMessage"] = "Username and/or Password Incorrect";
                DBClass.Lab3DBConnection.Close();
                return Page();

            }

        }
    }
}
