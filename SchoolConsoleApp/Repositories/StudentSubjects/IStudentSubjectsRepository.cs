using SchoolConsoleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolConsoleApp.Repositories.StudentSubjects
{
    interface IStudentSubjectsRepository
    {
        void GetPersonPoint(string studentId);
        void GetPersonPoint(string studentId, int subjectId);
        void GetPersonsPoint(int subjectId);
        void GetPersonsPoint();
        string AddPersonToSubject(int subjectId, string studentId);
        string SetPoint(string studentId, int subjectId, int point);
    }
}
