namespace Reed_Lab1.Pages.DataClasses
{
    public class Meeting
    {
        public int? MeetingID { get; set; }
        public String? MeetingPurpose { get; set; }

        public int InstructorID { get; set; }

        public int StudentID { get; set; }
    }
}
