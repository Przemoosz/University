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
            Console.WriteLine(t == s);
            // TestingCode.DbConnect();
            Field newField = new Field();
            // newField.DropTable();
            // newField.CreateTable();
            // newField.TableExists();
            // newField.DataInsertion();
            // newField.TableExists();
        }
    }
 
}