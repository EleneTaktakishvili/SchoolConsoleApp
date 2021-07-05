using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SchoolConsoleApp.ReadData;

namespace SchoolConsoleApp.Repositories.Students
{
    public class StudentRepository : IStudentRepository
    {
        string filePath = @"E:\source\repos\SchoolConsoleApp\SchoolConsoleApp\Data\StudentList.txt";
        string subjectStudentFilePath = @"E:\source\repos\SchoolConsoleApp\SchoolConsoleApp\Data\StudentSubject.txt";

        ReadDataFromTxt data = new ReadDataFromTxt();
        public void AddStudent(string firstName, string lastName, string studentId, int age, int gender)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(studentId))
            {
                Console.Write("Please Add All values");
            }          
            else
            {

                if (!data.StudentExists(studentId))
                {
                    try
                    {
                        string[] studentItem = new string[5];
                        studentItem[0] = firstName;
                        studentItem[1] = lastName;
                        studentItem[2] = studentId;
                        studentItem[3] = age.ToString();
                        studentItem[4] = gender.ToString();

                        File.AppendAllText(filePath, (string.Join(",", studentItem)) + Environment.NewLine);

                        Console.Write("Student Added Successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine($"Student with id {studentId} exists.");
                }
            }
        }
        public void RemoveStudent(string studentId)
        {
            var studentList = data.ReadStudent();
            if (!string.IsNullOrWhiteSpace(studentId))
            {
                if (studentList.Any(p => p.StudentId == studentId))
                {
                    try
                    {
                        //student delete
                        var newList = studentList.Where(p => p.StudentId != studentId).Select(p => p);

                        File.WriteAllText(filePath, string.Empty);

                        string[] studentItem = new string[5];
                        foreach (var item in newList)
                        {

                            studentItem[0] = item.StudentFirstName;
                            studentItem[1] = item.StudentLastName;
                            studentItem[2] = item.StudentId.ToString();
                            studentItem[3] = item.Age.ToString();
                            studentItem[4] = item.Gender.ToString();

                            File.AppendAllText(filePath, (string.Join(",", studentItem)) + Environment.NewLine);

                        }

                        //student subject delete

                        var StudentSubjectList = data.ReadStudentSubject();
                        var newStudentSubjectList = StudentSubjectList.Where(p => p.StudentId != studentId).Select(p => p);

                        File.WriteAllText(subjectStudentFilePath, string.Empty);

                        string[] studentSubjectItem = new string[3];

                        foreach (var item in newStudentSubjectList)
                        {

                            studentSubjectItem[0] = item.StudentId;
                            studentSubjectItem[1] = item.SubjectId.ToString();
                            studentSubjectItem[2] = item.Point.ToString();

                            File.AppendAllText(subjectStudentFilePath, (string.Join(",", studentSubjectItem)) + Environment.NewLine);

                        }
                        Console.Write("Student Removed from List.");
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                else
                {
                    Console.Write("Student Not Found.");
                }
            }
            else
            {
                Console.WriteLine("Please Add Student ID");
            }
        }       
    }
}
