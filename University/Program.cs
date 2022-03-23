using System;
using Npgsql;
namespace University
{
    internal class MainProgram
    {
        public static void Main(string[] args)
        {
            // Semester s = new Semester();
            // s.ShowConnectedSubjects();
            Start(new GetInputClass());
        }

        public static void Start(IDataInput input)
        {
            MenuEnum choosed;

            string typedNumberStr;
            int typedNumber;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose Mode:");
                Console.WriteLine("1. Create Field");
                Console.WriteLine("2. Create Semester");
                Console.WriteLine("3. Create Subject");
                Console.WriteLine("4. Show all Fields");
                Console.WriteLine("5. Show all Semester");
                Console.WriteLine("6. Show all Subjects");
                Console.WriteLine("7. Show Connected Semesters");
                Console.WriteLine("8. Show Connected Subjects");
                Console.WriteLine("9. Exit");
                typedNumberStr = input.GetInput();
                if (!Int32.TryParse(typedNumberStr, out typedNumber))
                {
                    continue;
                }

                choosed = typedNumber switch
                {
                    1 => MenuEnum.CreateField,
                    2 => MenuEnum.CreateSemester,
                    3 => MenuEnum.CreateSubject,
                    4 => MenuEnum.ShowAllFields,
                    5 => MenuEnum.ShowAllSemester,
                    6 => MenuEnum.ShowAllSubjects,
                    7 => MenuEnum.ShowConnectedSemester,
                    8 => MenuEnum.ShowConnectedSubjects,
                    _ => MenuEnum.Exit
                };
                switch (choosed)
                {
                    case MenuEnum.CreateField:
                        Menu.CreateField();
                        break;
                    case MenuEnum.CreateSemester:
                        Menu.CreateSemester();
                        break;
                    case MenuEnum.CreateSubject:
                        Menu.CreateSubject();
                        break;
                    case MenuEnum.ShowAllFields:
                        Field.AllFieldsInDatabase();
                        Console.ReadLine();
                        break;
                    case MenuEnum.ShowAllSemester:
                        Semester.AllSemesterInDatabase();
                        Console.ReadLine();
                        break;
                    case MenuEnum.ShowAllSubjects:
                        Subject s = new Subject();
                        s.FetchAllDataFromTable();
                        Console.ReadLine();
                        break;
                    case MenuEnum.ShowConnectedSemester:
                        Menu.ShowConnectedSemesters();
                        Console.ReadLine();
                        break;
                    case MenuEnum.ShowConnectedSubjects:
                        Menu.ShowConnectedSubjcts();
                        Console.ReadLine();
                        break;
                    case MenuEnum.Exit: 
                        System.Environment.Exit(1);
                        break;
                }
            }
        }
    }
}