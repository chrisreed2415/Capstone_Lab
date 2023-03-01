using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reed_Lab1.Pages.DataClasses;
using Reed_Lab1.Pages.DB;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Reed_Lab1.Pages
{
    public class OfficeAppointmentModel : PageModel
    {
        [BindProperty]
        [Required]
        public int OfficeA { get; set; }

        [BindProperty]
        [Required]
        public string OptionMin { get; set; }

        public String SelectMessage { get; set; }

        public List<SelectListItem> OfficeH { get; set; }


        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/Access");
            }

            // Populate the User SELECT control
            SqlDataReader IReader = DBClass.GeneralReaderQuery("SELECT * FROM OfficeAppointment Where InstructorID =" + HttpContext.Session.GetInt32("instruct"));

            OfficeH = new List<SelectListItem>();

            while (IReader.Read())
            {
                OfficeH.Add(
                    new SelectListItem(
                        IReader["OfficeHours"].ToString(),
                        IReader["OfficeNum"].ToString()));
            }
            DBClass.Lab3DBConnection.Close();
            return Page();
        }

        public void OnPostSingleSelect()
        {
            SelectMessage = "Your Office Hour Request is now in Queue!";
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetInt32("officeh", OfficeA);
            string selectQuery = "INSERT INTO Queue (Time, ProcessState, TimeRange, OfficeNum, InstructorID, StudentID) Values (CURRENT_TIMESTAMP, 'Waiting','" + OptionMin + "'," + OfficeA + "," + HttpContext.Session.GetInt32("instruct") + ","
                + HttpContext.Session.GetInt32("studentid") + ")";
            DBClass.SelectQuery(selectQuery);
            DBClass.Lab3DBConnection.Close();
            return RedirectToPage("/Index");

        }

    }
}
