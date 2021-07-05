using SchoolConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolConsoleApp.ReadData
{
    public class ReadDataFromTxt
    {
        string studentFilePath = @"E:\source\repos\SchoolConsoleApp\SchoolConsoleApp\Data\StudentList.txt";
        string subjectFilePath = @"E:\source\repos\SchoolConsoleApp\SchoolConsoleApp\Data\SubjectList.txt";
        string subjectStudentFilePath = @"E:\source\repos\SchoolConsoleApp\SchoolConsoleApp\Data\StudentSubject.txt";

        public void CheckPath(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.", nameof(path));
        }

        public int GetLastId()
        {
            Subject lineItem;
            String last = File.ReadLines(subjectFilePath).Last();
            string[] subjectItem = last.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            {
                lineItem = new Subject()
                {
                    SubjectId = Convert.ToInt32(subjectItem[0])
                };
            }
            return lineItem.SubjectId + 1;
        }
        public bool SubjectExists(string name)
        {
            List<Subject> list = new List<Subject>();
            string csvContents = File.ReadAllText(subjectFilePath);
            foreach (var line in csvContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] listItem = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                {
                    Subject lineItem = new Subject()
                    {
                        SubjectName = listItem[1]
                    };
                    list.Add(lineItem);
                }
            }
            if (list.Any(p => p.SubjectName == name))
            {
                return true;
            }
            return false;
        }
        public List<Student> ReadStudent()
        {
            List<Student> list = new List<Student>();
            string csvContents = File.ReadAllText(studentFilePath);
            foreach (var line in csvContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] csvItem = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                {
                    Models.Student lineItem = new Models.Student()
                    {
                        StudentFirstName = csvItem[0],
                        StudentLastName = csvItem[1],
                        StudentId = csvItem[2],
                        Age = Convert.ToInt32(csvItem[3]),
                        Gender = Convert.ToInt32(csvItem[4])

                    };
                    list.Add(lineItem);
                }
            }
            return list;
        }
        public List<StudentSubject> ReadStudentSubject()
        {
            List<StudentSubject> list = new List<StudentSubject>();
            string csvContents = File.ReadAllText(subjectStudentFilePath);
            foreach (var line in csvContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] csvItem = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                {
                    Models.StudentSubject lineItem = new Models.StudentSubject()
                    {
                        StudentId = csvItem[0],
                        SubjectId = Convert.ToInt32(csvItem[1]),
                        Point = Convert.ToInt32(csvItem[2])
                    };
                    list.Add(lineItem);
                }
            }
            return list;
        }
        public List<Subject> ReadSubject()
        {
            List<Subject> list = new List<Subject>();
            string csvContents = File.ReadAllText(subjectFilePath);
            foreach (var line in csvContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] csvItem = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                {
                    Models.Subject lineItem = new Models.Subject()
                    {
                        SubjectId = Convert.ToInt32(csvItem[0]),
                        SubjectName = csvItem[1]
                    };
                    list.Add(lineItem);
                }
            }
            return list;
        }
        public bool StudentExists(string studentId)
        {
            List<Student> list = new List<Student>();
            string csvContents = File.ReadAllText(studentFilePath);
            foreach (var line in csvContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] csvItem = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                {
                    Student lineItem = new Student()
                    {
                        StudentId = csvItem[2]
                    };
                    list.Add(lineItem);
                }
            }

            if (list.Any(p => p.StudentId == studentId))           
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SubjectExists(int subjectId)
        {
            List<Subject> list = new List<Subject>();
            string csvContents = File.ReadAllText(subjectFilePath);
            foreach (var line in csvContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] csvItem = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                {
                    Models.Subject lineItem = new Models.Subject()
                    {
                        SubjectId = Convert.ToInt32(csvItem[0])
                    };
                    list.Add(lineItem);
                }
            }

            if (list.Any(p => p.SubjectId == subjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool StudentAddedToSubject(int subjectId, string studentId)
        {
            var StudentSubjectList = ReadStudentSubject();

            if (StudentSubjectList.Any(p => (p.StudentId == studentId) && (p.SubjectId == subjectId)))
            {
                return true;
            }
            return false;
        }

    }
}
