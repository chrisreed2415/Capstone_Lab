using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reed_Lab1.Pages.DB;
using System;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Reed_Lab1.Pages.DataClasses;

namespace Reed_Lab1.Pages
{ 
    public class SummaryModel : PageModel
    {

        [BindProperty]
        public int InstructorID { get; set; }

        public int StudentID { get; set; }

        public List<SelectListItem> Instructors { get; set; }


       

        public IActionResult OnGet()
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


            // Populate the User SELECT control
            SqlDataReader IReader = DBClass.GeneralReaderQuery("SELECT * FROM Instructor");

            Instructors = new List<SelectListItem>();

            while (IReader.Read())
            {
                Instructors.Add(
                    new SelectListItem(
                        IReader["InstructorName"].ToString(),
                        IReader["InstructorID"].ToString()));
            }
            DBClass.Lab3DBConnection.Close();
            return Page();
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetInt32("instruct", InstructorID);
            string selectQuery = "SELECT InstructorID FROM Instructor WHERE InstructorName = '" + InstructorID + "'";
            DBClass.SelectQuery(selectQuery);
            DBClass.Lab3DBConnection.Close();
            return RedirectToPage("OfficeAppointment");

        }

    }
}
