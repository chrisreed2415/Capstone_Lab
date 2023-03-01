using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reed_Lab1.Pages.DataClasses;
using Reed_Lab1.Pages.DB;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Reed_Lab1.Pages
{
    public class FindInstructorModel : PageModel
    {
        public List<Instructor> InstructNames { get; set; }

        // Properties for Single Select Dropdown
        [BindProperty] 
        public int SelectedNumber { get; set; }

        public String SelectMessage { get; set; }

        public int StudentID { get; set; }

        public FindInstructorModel()
        {
            InstructNames = new List<Instructor>();

        }




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

  

            SqlDataReader InstructorReader = DBClass.InstructorReader();
            while (InstructorReader.Read())
            {
                InstructNames.Add(new Instructor
                {
                    InstructorID = Convert.ToInt32(InstructorReader["InstructorID"]),
                    InstructorName = InstructorReader["InstructorName"].ToString(),
                    User1 = InstructorReader["User1"].ToString()
                    

                });
            }


            DBClass.Lab3DBConnection.Close();
            return Page();
            
        }



        public void OnPostSingleSelect()
        {
            SelectMessage = "Selection Choice was Value: " + SelectedNumber;
            HttpContext.Session.SetInt32("instructor", SelectedNumber);
        }



        public IActionResult OnPost()
        {
            DBClass.SingleOffice(SelectedNumber);
            DBClass.Lab3DBConnection.Close();
            return RedirectToPage("Office");

        }
        
    }
}
