using SchoolConsoleApp.ReadData;
using System;
using System.IO;

namespace SchoolConsoleApp.Repositories.Subjects
{
    public class SubjectRepository : ISubjectRepository
    {

        string filePath = @"E:\source\repos\SchoolConsoleApp\SchoolConsoleApp\Data\SubjectList.txt";
        ReadDataFromTxt data = new ReadDataFromTxt();
        public void AddSubject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Please Add Subject Name");
            }
            else
            {
                if (!data.SubjectExists(name))
                {
                    try
                    {

                        string[] subjectItem = new string[2];
                        subjectItem[0] = data.GetLastId().ToString();
                        subjectItem[1] = name;

                        File.AppendAllText(filePath, (string.Join(",", subjectItem)) + Environment.NewLine);

                        Console.Write($"Subject {name} Added Successfully");

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                Console.Write($"Subject {name} already exists!");
            }
        }
       
    }
}
