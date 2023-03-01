using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reed_Lab1.Pages.DB;
using System.Data.SqlClient;

namespace Reed_Lab1.Pages.OfficeHourQueue
{
    public class InstructorQueueModel : PageModel
    {
        public int InstructorID { get; set; }
        public ActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/Access");
            }

            SqlDataReader idReader = DBClass.GeneralReaderQuery("Select Instructor.InstructorID from Instructor where Instructor.User1 = '" + HttpContext.Session.GetString("username") + "'");
            while (idReader.Read())
            {
                InstructorID = Convert.ToInt32(idReader["InstructorID"]);
                HttpContext.Session.SetInt32("instructorid", InstructorID);
            }
            DBClass.Lab3DBConnection.Close();
            return Page();
        }

        //public ActionResult OnPostCheckIn()
        //{
        //    SqlDataReader CheckReader = DBClass.GeneralReaderQuery("UPDATE NOTIFICATIONS SET PROCESS CHANGE = 'Ready' WHERE InstructorID =" + HttpContext.Sess);
        //    while (CheckReader.Read())
        //    {
        //        InstructorID = Convert.ToInt32(CheckReader["InstructorID"]);
        //        HttpContext.Session.SetInt32("instructorid", InstructorID);
        //    }
        //    DBClass.Lab3DBConnection.Close();
        //    return Page();
        //}

        //public ActionResult OnPostRemove()
        //{

        //}
    }
}
