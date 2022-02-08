using System;
using Npgsql;
namespace University
{
    internal class MainProgram
    {
        public static void Main(string[] args)
        {
            // bool[] t = new bool[2] {false, false};
            // bool[] s = new bool[2] {false, false};
            
            Subject s = new Subject();
            // s.ExamGradeProperty = 2.0f;
            // s.ExceriseGradeProperty = 4.5f;
            // s.LaboratoryGradeProperty = 2.5f;
            s.DataInsertion();
            // s.AverageCalculate();
            //
            // Console.WriteLine(s.AverageProperty);
            // Field newField = new Field();
            // newField.CreateField();
            // newField.LoadFieldFromDatabase();
            // Console.WriteLine(newField.nameProperty);
            // newField.ObtainedEctsRecalculate();
            // newField.DropTable();
            // newField.CreateTable();
            // newField.CreateField();
            //Semester newSemester = new Semester("ED", 1);
            //newSemester.CreateSemester();
            // newSemester.DataInsertion();
            //newSemester.DropTable();
            // newSemester.CreateTable();
            // s.CreateTable();
            // newSemester.CreateSemester();
            // newSemester.CreateSemester();
            // newField.ShowConnectedSemesters();
            // Field.AllFieldsInDatabase();
            //newField.ShowConnectedSemesters();
            //newField.CreateTable();
            //newField.DataInsertion();
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
            // Semester sem = new Semester("MED",1);

            // sem.DropTable();
            // sem.CreateTable();
            // sem.DataInsertion();
            // sem.FetchDataFromDb();
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