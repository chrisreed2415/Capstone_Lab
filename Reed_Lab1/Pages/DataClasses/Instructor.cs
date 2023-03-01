using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Reed_Lab1.Pages.DataClasses
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public String? InstructorName { get; set; }
        public String? Email { get; set; }

        public String? User1 { get; set; }


    }
}
