using System;
using Npgsql;
namespace University
{
    internal class MainProgram
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("".Length);
            // TestingCode.DbConnect();
            Field newField = new Field();
            //newField.ectsTotalProperty = 300;
            //Console.WriteLine(newField.ectsTotalProperty);
            // int year = DateTime.Now.Year;
            // short yearshort = Convert.ToInt16(year);
            // Console.WriteLine(yearshort.GetType());
            newField.CreateField();
            //Console.WriteLine(newField.ectsTotalProperty);
        }
    }
 
}