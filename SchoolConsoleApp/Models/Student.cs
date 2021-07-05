
namespace SchoolConsoleApp.Models
{
    public class Student
    {
        public string StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }

        // public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
