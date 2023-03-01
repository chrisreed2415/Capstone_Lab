using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reed_Lab1.Pages.DataClasses;
using Reed_Lab1.Pages.DB;
using System.Data.SqlClient;

namespace Reed_Lab1.Pages
{
    public class OfficeModel : PageModel
    {
        [BindProperty]
        public List<OfficeAppointment> OfficeHour { get; set; }


        // Properties for Single Select Dropdown
        [BindProperty] public int SelectedOffice { get; set; }

        public String SelectMessage { get; set; }

        public int OfficeNum { get; set; }

        public OfficeModel()
        {
            OfficeHour = new List<OfficeAppointment>();
        }


        public void OnGet(int instructorid)
        {
            HttpContext.Session.SetInt32("instructorid", instructorid);
            SqlDataReader OfficeReader = DBClass.SingleOffice(instructorid);
            while (OfficeReader.Read())
            {
                OfficeHour.Add(new OfficeAppointment
                {
                    OfficeNum = Convert.ToInt32(OfficeReader["OfficeNum"].ToString()),
                    OfficeHours = OfficeReader["OfficeHours"].ToString(),
                    InstructorID = Int32.Parse(OfficeReader["InstructorID"].ToString())
                    

                }) ;
            }

            DBClass.Lab3DBConnection.Close();

        }

        public void OnPostSingleSelect()
        {
            SelectMessage = "Selection Choice was Value: " + SelectedOffice;
            HttpContext.Session.SetInt32("officenum", SelectedOffice);
        }

        public IActionResult OnPost()
        {
            DBClass.SingleOffice(SelectedOffice);
            DBClass.Lab3DBConnection.Close();
            return RedirectToPage("Meeting");

        }


    }
}
