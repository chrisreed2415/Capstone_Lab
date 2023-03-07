using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reed_Lab1.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }


    /*
       /* 
     public void OnPostSearchedInstructor()
        {
            if (SearchedInstructor == null)
            {
                SearchMessage = "Invalid Input, Please Enter First and Last Name";
            }
            else
            {
                String[] instructorNames = SearchedInstructor.Split(" ");
                if (instructorNames.Length != 2)
                {
                    SearchMessage = "Invalid Input, Please Enter First and Last Name";
                }
                else
                {
                    SqlDataReader SIReader = DBClass.ReadInstructorAndDisplayOH(@instructorNames[0], @instructorNames[1]);
                    while (SIReader.Read())
                    {
                        SelectedOHList.Add(new SelectedInstructorOH
                        {
                            instructorFirstName = SIReader["instructorFirstName"].ToString(),
                            instructorLastName = SIReader["instructorLastName"].ToString(),
                            ohDate = SIReader["ohDate"].ToString(),
                            ohTimes = SIReader["ohTimes"].ToString(),
                            ohLocation = SIReader["ohLocation"].ToString(),
                            ohWaitLocation = SIReader["ohWaitLocation"].ToString(),
                        });

                    }
                    DBClass.SchoolDBConnection.Close();

                }
            }

        }
     */


    
}
