using Microsoft.AspNetCore.Mvc;

namespace Reed_Lab1.Pages.DataClasses
{
    public class OfficeAppointment
    {
        public int OfficeNum { get; set; }
        public String? OfficeLocation { get; set; }
        public String? OfficeHours { get; set; }
        public int? InstructorID { get; set; }
    }
}
