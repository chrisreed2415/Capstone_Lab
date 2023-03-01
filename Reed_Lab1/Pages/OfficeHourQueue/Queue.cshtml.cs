using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reed_Lab1.Pages.DataClasses;
using Reed_Lab1.Pages.DB;
using System.Data.SqlClient;

namespace Reed_Lab1.Pages
{
    public class QueueModel : PageModel
    {
        [BindProperty]
        public int OfficeA { get; set; }

        [BindProperty]
        public List<OfficeAppointment> officequeue { get; set; }

        public QueueModel()
        {
            officequeue = new List<OfficeAppointment>();
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/Login/Access");
            }

            // Populate the User SELECT control
            SqlDataReader IReader = DBClass.GeneralReaderQuery("SELECT * FROM OfficeAppointment Where InstructorID =" + HttpContext.Session.GetInt32("iqueue"));

            while (IReader.Read())
            {
                officequeue.Add(new OfficeAppointment
                {
                    OfficeNum = Convert.ToInt32(IReader["OfficeNum"].ToString()),
                    OfficeHours = IReader["OfficeHours"].ToString(),
                    InstructorID = Int32.Parse(IReader["InstructorID"].ToString())

                });
            }
            DBClass.Lab3DBConnection.Close();
            return Page();
        }
    }
}
