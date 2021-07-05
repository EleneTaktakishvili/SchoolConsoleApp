
namespace SchoolConsoleApp.Repositories.Students
{
    interface IStudentRepository
    {
        void AddStudent(string firstName, string lastName, string studentId, int age, int gender);
        void RemoveStudent(string studentId);

    }
}
