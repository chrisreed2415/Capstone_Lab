using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reed_Lab1.Pages.DataClasses;
using Reed_Lab1.Pages.DB;
using System.Data.SqlClient;

namespace Reed_Lab1.Pages.OfficeHourQueue
{
    public class NotifsModel : PageModel
    {

        public int StudentID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/Access");
            }

            SqlDataReader idReader = DBClass.GeneralReaderQuery("Select Student.StudentID from Student where Student.User1 = '" + HttpContext.Session.GetString("username") + "'");
            while (idReader.Read())
            {
                StudentID = Convert.ToInt32(idReader["StudentID"]);
                HttpContext.Session.SetInt32("studentid", StudentID);
            }
            DBClass.Lab3DBConnection.Close();
            return Page();
        }
    }
}
