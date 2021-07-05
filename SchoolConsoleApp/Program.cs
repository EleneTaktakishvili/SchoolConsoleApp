using SchoolConsoleApp.Repositories.Students;
using SchoolConsoleApp.Repositories.Subjects;
using SchoolConsoleApp.Repositories.StudentSubjects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SchoolConsoleApp
{
    class Program
    {
        private static IStudentRepository _studentRepository;
        private static ISubjectRepository _subjectRepository;
        private static IStudentSubjectsRepository _StudentSubjectRepository;

        public static void Main(string[] args)
        {

            PrintCommand();
            Command(ReadCommand());
            Console.ReadKey();
        }
        private static string[] ReadCommand()
        {
            string input = Console.ReadLine();

            //remove whitespace
            input = Regex.Replace(input, @"\s+", String.Empty);

            List<string> delims = new List<string> { "-", "," };
            string[] out1 = input.Split(delims.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return out1;
        }
        private static void Command(string[] command)
        {
            _studentRepository = new StudentRepository();
            _subjectRepository = new SubjectRepository();
            _StudentSubjectRepository = new StudentSubjectsRepository();

            switch (command[0])
            {
                case "AddPerson":
                    if (!CheckID(command[3]))
                    {
                        Console.WriteLine("ID must be 11 digit numer.");
                    }
                    else if (!CheckAge(Convert.ToInt32(command[4])))
                    {
                        Console.WriteLine("Age must be in 18-29 range.");
                    }
                    else if (!CheckGender(Convert.ToInt32(command[5])))
                    {
                        Console.WriteLine("1.Female 2.Male");
                    }
                    else
                    {
                        _studentRepository.AddStudent(command[1], command[2], command[3], Convert.ToInt32(command[4]), Convert.ToInt32(command[5]));

                    }
                    break;

                case "RemovePerson":
                    Console.WriteLine("Are you sure you want to remove student? [y/n]");
                    string option = Console.ReadLine();
                    if (option == "y")
                    {
                        _studentRepository.RemoveStudent(command[1]);
                    }
                    else
                    {
                        return;
                    }
                    break;

                case "AddSubject":
                    _subjectRepository.AddSubject(command[1]);
                    break;

                case "AddPersontoSubject":
                    Console.WriteLine(_StudentSubjectRepository.AddPersonToSubject(Convert.ToInt32(command[1]), command[2]));
                    break;

                case "SetPoint":
                    Console.WriteLine(_StudentSubjectRepository.SetPoint(command[1], Convert.ToInt32(command[2]), Convert.ToInt32(command[3])));
                    break;

                case "GetPersonPoint":
                    if (command.Length == 2)
                    {
                        _StudentSubjectRepository.GetPersonPoint(command[1]);
                    }
                    else
                    {
                        _StudentSubjectRepository.GetPersonPoint(command[1], Convert.ToInt32(command[2]));
                    }
                    break;

                case "GetPersonsPoint":
                    if (command.Length == 2)
                    {
                        _StudentSubjectRepository.GetPersonsPoint(Convert.ToInt32(command[1]));
                    }
                    else
                    {
                        _StudentSubjectRepository.GetPersonsPoint();
                    }
                    break;

                default:
                    Console.WriteLine("Please, Choose Correct Command.");
                    break;
            }
        }
        private static void PrintCommand()
        {
            Console.WriteLine("1.Add Person- FirstName, LastName, PersonId, Age, Gender: 1.Female, 2.Male");
            Console.WriteLine("2.Remove Person- PersonId");
            Console.WriteLine("3.Add Subject- Subject Name");
            Console.WriteLine("4.Add Person to Subject- SubjectId, PersonId");
            Console.WriteLine("5.Set Point- PersonId, SubjectId, Point");
            Console.WriteLine("6.Get Person Point- PersonId");
            Console.WriteLine("7.Get Person Point- PersonId, SubjectId");
            Console.WriteLine("8.Get Persons Point- SubjectId");
            Console.WriteLine("9.Get Persons Point");
            Console.WriteLine("====================================");

        }


        #region 
        
       /// <summary> Check if age is in range 18-29 </summary>
       /// <param name="age"></param>
       /// <returns></returns>
       static bool CheckAge(int age)
        {
            string pattern = @"\b(1[8-9]|2[0-9])\b";
            Regex rg = new Regex(pattern);
            if (rg.IsMatch(age.ToString()))
            {
                return true;
            }
            return false;
        }

        /// <summary> Check if ID is 11 number digit </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        static bool CheckID(string studentId)
        {
            string pattern = @"\d{11}";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(studentId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


       /// <summary>Check if gender is 1 0r 2 </summary>
       /// <param name="gender"></param>
       /// <returns></returns>
       static bool CheckGender(int gender)
        {
            string pattern = @"\b([1-2])\b";
            Regex rg = new Regex(pattern);
            if (rg.IsMatch(gender.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
