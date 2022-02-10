using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using University;
using Npgsql;

namespace SubjectTest;

[TestClass]
public class NamePropertyTests
{
    // Name property should allows string with Length between 0 and 100
    // Blank name are not allowed, too long name is also not allowed!
    [TestMethod]
    public void Name_Provided_Correctly()
    {
        // Testing Set and get property
        
        // Arrange Section
        Subject testSubject = new Subject();
        string name = "Teoria Obwodów I";
        
        // Act Section
        testSubject.NameProperty = name;
        
        // Assert Section
        Assert.AreEqual(name,testSubject.NameProperty);
    }
    [TestMethod]
    public void Name_Provided_Blank()
    {
        // Testing protection against providing blank subject name
        // Property should raise Argument Exception
        
        // Arrange Section
        Subject testSubject = new Subject();
        string name = "";
        
        // Act and Assert SectionSection
        Assert.ThrowsException<ArgumentException>(() => testSubject.NameProperty = name);
    }
    
    [TestMethod]
    public void Name_Provided_Too_Long()
    {
        // Testing protection against providing too long subject name (over 100 chars)
        // Property should raise Argument Exception
        
        // Arrange Section
        Subject testSubject = new Subject();
        string name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam fringilla quam ac ipsum orci aliquam. Bye!";
        
        // Act and Assert SectionSection
        Assert.ThrowsException<ArgumentException>(() => testSubject.NameProperty = name);
    }
}

[TestClass]
public class GradeTests
{
    // Testing Grade Property. 
    // According to polish grade system in universities possible grades are: 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0
    // Addition is that: exam, laboratory, exercise grade can be 0.0 if subject does not have ex. Exam 
    // Testing all 4 Properties at one time, because the logic is the same

    [TestMethod]
    public void Grade_Provided_Correctly()
    {
        // Testing set and get proeperty
        
        // Arrange Section
        Subject testSubject = new Subject();
        float grade = 3.5f;
        
        // Act Section
        testSubject.ExamGradeProperty = grade;
        testSubject.EndingGradeProperty = grade;
        testSubject.ExerciseGradeProperty = grade;
        testSubject.LaboratoryGradeProperty = grade;
        
        // Assert Section
        Assert.AreEqual(grade,testSubject.ExamGradeProperty);
        Assert.AreEqual(grade,testSubject.EndingGradeProperty);
        Assert.AreEqual(grade,testSubject.ExerciseGradeProperty);
        Assert.AreEqual(grade,testSubject.LaboratoryGradeProperty);
    }
    [TestMethod]
    public void Grade_Provided_Not_Correctly()
    {
        // Testing set and get proeperty
        
        // Arrange Section
        Subject testSubject = new Subject();
        float grade = 4.2f;
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testSubject.EndingGradeProperty = grade);
        Assert.ThrowsException<ArgumentException>(() => testSubject.ExerciseGradeProperty = grade);
        Assert.ThrowsException<ArgumentException>(() => testSubject.ExamGradeProperty = grade);
        Assert.ThrowsException<ArgumentException>(() => testSubject.LaboratoryGradeProperty = grade);
    }
}

[TestClass]
public class AverageTest
{
    // Testing Average calculation method
    [TestMethod]
    public void Average_All_Grades()
    {
        // Testing average calculation after providing 3 grades
        
        // Arrange Section
        Subject testSubject = new Subject();
        float firstGrade = 2.0f;
        float secondGrade = 2.5f;
        float thirdGrade = 4.5f;
        double average;
        
        // Act Section
        testSubject.LaboratoryGradeProperty = firstGrade;
        testSubject.ExamGradeProperty = secondGrade;
        testSubject.ExerciseGradeProperty = thirdGrade;
        average = Math.Round((firstGrade + secondGrade + thirdGrade) / 3, 3);
        testSubject.AverageCalculate();
        
        // Assert Section
        Assert.AreEqual((float)average,testSubject.AverageProperty);
    }
    [TestMethod]
    public void Average_Two_Grades()
    {
        // Testing Average calculate method
        // This time using 2 grades and one 0.0

        // Arrange Section
        Subject testSubject = new Subject();
        float gradeOne = 2.5f;
        float gradeTwo = 3.0f;
        
        // Act Section
        testSubject.ExamGradeProperty = gradeOne;
        testSubject.LaboratoryGradeProperty = gradeTwo;
        testSubject.AverageCalculate();
        float expected = (float) Math.Round((gradeOne + gradeTwo) / 2, 3);
        // Assert Section
        Assert.AreEqual(expected,testSubject.AverageProperty);

    }
    [TestMethod]
    public void Average_One_Grade()
    {
        // Testing Average calculate method
        // This time using 1 grades and two 0.0
        
        // Arrange Section
        Subject testSubject = new Subject();
        float avg = 4.0f;
        
        // Act Section
        testSubject.ExamGradeProperty = avg;
        testSubject.AverageCalculate();
        
        // Assert Section
        Assert.AreEqual(avg,testSubject.AverageProperty);
    }
    [TestMethod]
    public void Average_Zero_Grades()
    {
        // Testing Average calculate method, grades are not provided
        // method should average value to 0.0f
        
        // Arrange Section
        Subject testSubject = new Subject();
        float expected = 0.0f;
        
        // Act Section
        testSubject.AverageCalculate();    
        
        // Assert Section
        Assert.AreEqual(expected,testSubject.AverageProperty);

    }
    [TestMethod]
    public void Average_Random_Grades()
    {
        // Testing Random input average calculate
        
        // Arrange Section
        int index;
        int n = 0;
        float result;
        float sum = 0.0f;
        
        Random gen = new Random();
        float[] grades = new[] {0.0f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f};
        Subject testSubject = new Subject();
        
        Action<float> avgStep = f =>
        {
            if (f!=0.0)
            {
                n++;
                sum += f;
            }
        }; 

        
        // Act Section
        
        index = gen.Next(0, 8);
        testSubject.ExamGradeProperty = grades[index];
        avgStep(grades[index]);
        
        index = gen.Next(0, 8);
        testSubject.LaboratoryGradeProperty = grades[index];
        avgStep(grades[index]);
        
        index = gen.Next(0, 8);
        testSubject.ExerciseGradeProperty = grades[index];
        avgStep(grades[index]);
        testSubject.AverageCalculate();
        if (n != 0)
        {
            result = (float) Math.Round(sum / n, 3);
        }
        else
        {
            result = 0.0f;
        }
        
        // Assert Section
        Assert.AreEqual(result,testSubject.AverageProperty);
    }
}

[TestClass]
public class DataBaseTest
{
    // Testing all methods connected with database
    // We assume that TestUtils return always true values

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        // Initialize test class
        TestUtils.TableDrop("field");
        TestUtils.TableDrop("semester");
        TestUtils.TableDrop("subjects");
        
        // Creating default field and default semester table
        Field field = new Field();
        field.CreateTable();
        field.DataInsertion();
        Semester semester = new Semester("ED", 1);
        semester.CreateTable();
        semester.DataInsertion();
    }

    [TestMethod]
    public void Testing_Table_Creation()
    {
        // Testing create table method
        // Method should create table in database
        
        // Arrange Section
        Subject testSubject = new Subject();

        // Act Section
        if (TestUtils.TableExists())
        {
            throw new Exception("Table already exists. Re-run test class or contact developer");
        }
        testSubject.CreateTable();
        
        // Assert Section
        Assert.AreEqual(true,TestUtils.TableExists());
    }

    [TestMethod]
    public void Testing_Table_Drop()
    {
        // Testing drop table method from subject object
        // method should drop existing database
        
        // Arrange Section
        Subject testSubject = new Subject();

        TestUtils.CreateSimpleTableToDrop();
        
        // Act Section
        if (!TestUtils.TableExists())
            throw new Exception(
                "Table 'subjects' should exists. Contact developer to fix the problem, or check postgresql settings");
        testSubject.DropTable();
                
        // Assert Section
        Assert.AreEqual(false,TestUtils.TableExists());
    }

    [TestMethod]
    public void Testing_Table_Exists()
    {
        // Testing exists method from subject object
        // method should return true because table will exist
        
        // Arrange Section
        Subject testSubject = new Subject();
        TestUtils.CreateSimpleTableToDrop();
        bool result;
        
        // Act Section
        result = testSubject.TableExists();

        // Assert Section
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Testing_Table_Not_Exists()
    {
        // Testing exists method from subject object
        // method should return true because table will exist
        
        // Arrange Section
        Subject testSubject = new Subject();
        TestUtils.TableDrop("subjects");
        bool result;
        
        // Act Section
        result = testSubject.TableExists();

        // Assert Section
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Testing_Insertion_To_Table()
    {
        // Testing insertion. First methods catch data from user, then method insert those data
        // to DataBase. After fetching, data should not be changed.
        
        // Arrange Section
        Subject testSubject = new Subject();
        string testName = "Test";
        short testEcts = 8;
        float testAverage;
        float testExam = 3.0f;
        float testLab = 2.5f;
        float testExe = 4.5f;
        float testEnd = 5.0f;
        int testSubjectId = 1;
        testSubject.CreateTable();
        testSubject.NameProperty = testName;
        testSubject.EctsProperty = testEcts;
        testSubject.ExamGradeProperty = testExam;
        testSubject.LaboratoryGradeProperty = testLab;
        testSubject.ExerciseGradeProperty = testExe;
        testSubject.EndingGradeProperty = testEnd;
        testSubject.AverageCalculate();
        testAverage = testSubject.AverageProperty;
        
        // Act Section 
        testSubject.DataInsertion();
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            string cmd = "SELECT * FROM subjects;";
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            { 
                // Assert Section
                try
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Assert.AreEqual(testSubjectId, reader.GetInt32(0));
                            Assert.AreEqual(testName, reader.GetString(1));
                            Assert.AreEqual(testEcts,reader.GetInt16(2));
                            Assert.AreEqual(testAverage, (float) reader.GetDouble(3));
                            Assert.AreEqual(testEnd, (float) reader.GetDouble(4));
                            Assert.AreEqual(testExam, (float) reader.GetDouble(5));
                            Assert.AreEqual(testLab, (float) reader.GetDouble(6));
                            Assert.AreEqual(testExe, (float) reader.GetDouble(7));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
    [TestCleanup]
    public void TestCleanUp()
    {
        // Cleanup after each test
        TestUtils.TableDrop("subjects");
    }
    [AssemblyCleanup]
    public static void AssemblyCleanUp()
    {
        // After tests cleanup
        TestUtils.TableDrop("field");
        TestUtils.TableDrop("semester");
        TestUtils.TableDrop("subjects");
    }
}