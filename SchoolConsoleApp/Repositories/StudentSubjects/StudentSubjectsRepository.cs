using SchoolConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SchoolConsoleApp.ReadData;
namespace SchoolConsoleApp.Repositories.StudentSubjects
{
    public class StudentSubjectsRepository : IStudentSubjectsRepository
    {
        string subjectStudentFilePath = @"E:\source\repos\SchoolConsoleApp\SchoolConsoleApp\Data\StudentSubject.txt";

        ReadDataFromTxt data = new ReadDataFromTxt();
        public string AddPersonToSubject(int subjectId, string studentId)
        {
            if (data.StudentExists(studentId))
            {
                if (data.SubjectExists(subjectId))
                {
                    if (data.StudentAddedToSubject(subjectId, studentId))
                    {
                        return "This Student Already Added to Subject";
                    }
                    else
                    {

                        try
                        {
                            string[] studentsubjectItem = new string[3];
                            studentsubjectItem[0] = studentId;
                            studentsubjectItem[1] = subjectId.ToString();
                            studentsubjectItem[2] = "0";

                            File.AppendAllText(subjectStudentFilePath, (string.Join(",", studentsubjectItem)) + Environment.NewLine);

                            return "Student Added to Subject Successfully";
                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }
                    }
                }
                return "Subject Not Found";
            }
            return "Student Not Found.";
        }
        public string SetPoint(string studentId, int subjectId, int point)
        {
            if (data.StudentExists(studentId))
            {
                if (data.SubjectExists(subjectId))
                {

                    try
                    {
                        var StudentSubjectList = data.ReadStudentSubject();
                        var newStudentSubjectList = StudentSubjectList.Where(p => (p.StudentId == studentId) && (p.SubjectId == subjectId)).Select(p => p);

                        var result = StudentSubjectList.Except(StudentSubjectList);

                        File.WriteAllText(subjectStudentFilePath, string.Empty);

                        string[] studentItem = new string[3];
                        foreach (var item in result)
                        {

                            studentItem[0] = item.StudentId;
                            studentItem[1] = item.SubjectId.ToString();
                            studentItem[2] = item.Point.ToString();

                            File.AppendAllText(subjectStudentFilePath, (string.Join(",", studentItem)) + Environment.NewLine);
                        }
                        studentItem[0] = studentId;
                        studentItem[1] = subjectId.ToString();
                        studentItem[2] = point.ToString();

                        File.AppendAllText(subjectStudentFilePath, (string.Join(",", studentItem)) + Environment.NewLine);

                        return "Point Set Successfully";
                    }
                    catch (Exception ex)
                    {

                        return ex.Message;
                    }

                }
                return "Subject Not Found.";
            }
            return "Student Not Found.";
        }
        public void GetPersonPoint(string studentId)
        {
            var StudentSubjectList = data.ReadStudentSubject();
            var SubjectList = data.ReadSubject();

            if (data.StudentExists(studentId))
            {
                try
                {

                    if (StudentSubjectList.Any(p => p.StudentId == studentId))
                    {
                        var result = SubjectList.Join(StudentSubjectList.Where(p => p.StudentId == studentId),
                            subject => subject.SubjectId,    // outerKeySelector
                            studentSubject => studentSubject.SubjectId,  // innerKeySelector
                              (subject, studentSubject) => new StudentSubject() // result selector
                              {
                                  SubjectName = subject.SubjectName,
                                  Point = studentSubject.Point
                              });

                        foreach (var item in result)
                        {
                            Console.WriteLine($"{ item.SubjectName }- {item.Point}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Student Not Added to Any Subject.");
                    }
                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }

        }
        public void GetPersonPoint(string studentId, int subjectId)
        {
            var StudentSubjectList = data.ReadStudentSubject();
            var SubjectList = data.ReadSubject();

            if (data.StudentExists(studentId))
            {
                if (data.SubjectExists(subjectId))
                {
                    try
                    {
                        if (StudentSubjectList.Any(p => (p.StudentId == studentId) && (p.SubjectId == subjectId)))
                        {
                            var result = StudentSubjectList.Where(p => (p.StudentId == studentId) && (p.SubjectId == subjectId))
                                .Select(o => new StudentSubject()
                                {
                                    Point = o.Point
                                }).Single();

                            Console.WriteLine(result.Point);
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Subject Not Found.");
                }
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }
        }
        public void GetPersonsPoint(int subjectId)
        {
            var StudentSubjectList = data.ReadStudentSubject();
            var studentList = data.ReadStudent();

            if (data.SubjectExists(subjectId))
            {
                if (StudentSubjectList.Any(p => p.SubjectId == subjectId))
                {
                    try
                    {
                        var result = studentList.Join(StudentSubjectList.Where(p => p.SubjectId == subjectId),
                           student => student.StudentId,    // outerKeySelector
                           studentSubject => studentSubject.StudentId,  // innerKeySelector
                             (student, studentSubject) => new StudentSubject() // result selector
                             {
                                 StudentName = student.StudentFirstName,
                                 Point = studentSubject.Point
                             });

                        foreach (var item in result)
                        {
                            Console.WriteLine($"Student {item.StudentName} - { item.Point}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Subject Not Found.");
            }

        }
        public void GetPersonsPoint()
        {
            var StudentSubjectList = data.ReadStudentSubject();
            var SubjectList = data.ReadSubject();
            var studentList = data.ReadStudent();

            try
            {

                var result = SubjectList.Join(StudentSubjectList,
                       subject => subject.SubjectId,    // outerKeySelector
                       studentSubject => studentSubject.SubjectId,  // innerKeySelector
                         (subject, studentSubject) => new // result selector
                         {
                             a = subject,
                             b = studentSubject
                         })
                    .Join(studentList,
                     student => student.b.StudentId,    // outerKeySelector
                     studentSubject => studentSubject.StudentId,  // innerKeySelector
                     (student, studentSubject) => new StudentSubject() // result selector
                     {
                         SubjectName = student.a.SubjectName,
                         StudentName = studentSubject.StudentFirstName,
                         Point = student.b.Point,

                     }
                    );
                var groups = result.GroupBy(x => x.SubjectName);

                foreach (var groupingByClassA in groups)
                {
                    string propertyIntOfClassA = groupingByClassA.Key;
                    Console.WriteLine(propertyIntOfClassA);

                    foreach (var item in groupingByClassA)
                    {
                        Console.WriteLine($"{item.StudentName} - {item.Point}");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
