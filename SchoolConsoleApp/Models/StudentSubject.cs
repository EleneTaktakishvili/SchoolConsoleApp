
namespace SchoolConsoleApp.Models
{
    public class StudentSubject
    {
        public string StudentId { get; set; }
        public int SubjectId { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int Point { get; set; }

        //public virtual Student Student { get; set; }
        //public virtual Subject Subject { get; set; }

    }
}
