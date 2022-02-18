using System;
using Npgsql;
namespace University
{
    internal class MainProgram
    {
        public static void Main(string[] args)
        {
            // Menu.ShowConnectedSemesters();
            Semester s = new Semester();
            s.LoadDataFromDatabase(new GetInputClass());
            Console.WriteLine(s.NameProperty);
        }
    }
 
}