using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reed_Lab1.Pages.DataClasses;
using Reed_Lab1.Pages.DB;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Reed_Lab1.Pages
{
    public class MeetingModel : PageModel
    {
        [BindProperty]
        [Required]
        public Meeting MeetingEntry { get; set; }

        public MeetingModel() 
        { 
            MeetingEntry = new Meeting();
        }
         
           
 


        public void OnGet(int instructorid)
        {
            HttpContext.Session.SetInt32("instructorid", instructorid);
            SqlDataReader MeetingReader = DBClass.SingleMeeting(instructorid);
            while (MeetingReader.Read())
            {
                MeetingEntry.MeetingPurpose = MeetingReader["MeetingPurpose"].ToString();
                MeetingEntry.InstructorID = instructorid;
                MeetingEntry.StudentID = Int32.Parse(MeetingReader["StudentID"].ToString());
            }

            DBClass.Lab3DBConnection.Close();
        }


        public IActionResult OnPostPopulateHandler()
        {
            ModelState.Clear();

            return Page();

        }

        public IActionResult OnPost()
        {

            string insertQuery = "insert into Meeting (MeetingPurpose,InstructorID,StudentID,OfficeNum) VALUES ('";
            insertQuery += MeetingEntry.MeetingPurpose + "'," + HttpContext.Session.GetInt32("instructor") + ",";
            insertQuery += HttpContext.Session.GetInt32("studentid") + "," + HttpContext.Session.GetInt32("officenum") + ")";

            DBClass.SelectQuery(insertQuery);

            DBClass.Lab3DBConnection.Close();

            return RedirectToPage("StudentInfo");
        }
    }
}
