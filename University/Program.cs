using System;
using Npgsql;
namespace University
{
    internal class MainProgram
    {
        public static void Main(string[] args)
        {
            bool[] t = new bool[2] {false, false};
            bool[] s = new bool[2] {false, false};
            Field newField = new Field();
            newField.CreateTable();
            newField.DataInsertion();
            // newField.DropTable();
            // newField.CreateTable();
            // Random rndInt = new Random();
            // for (int xxx =0; xxx<50; xxx++)
            // {
            //     Console.WriteLine(rndInt.Next(1,10));
            // }
            // newField.TableExists();
            //
            // newField.DataInsertion();
            // Console.WriteLine(newField.fieldIdProperty);
            Semester sem = new Semester("MED",1);
            
            sem.DropTable();
            sem.CreateTable();
            sem.DataInsertion();
            sem.FetchDataFromDb();
            //sem.ProvideName();
            //Console.WriteLine(sem.nameProperty);
            //sem.CreateTable();
            // sem.DropTable();
            //sem.TableExists();
            // sem.nameProperty = "Semester I";
            // Console.WriteLine(sem.nameProperty);
        }
    }
 
}