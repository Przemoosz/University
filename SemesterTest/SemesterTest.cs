using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using University;
namespace SemesterTest;

[TestClass]
public class NameTesting
{
    [TestMethod]
    public void Name_Provided_Correctly_ED_Title()
    {
        // Testing name set and get property
        // Testing all names available for Engineer Degree title
        
        // Arrange section
        Semester testSemester = new Semester("Engineer Degree");
        string[] testingSemesterNames = new string[7] {"Semester I","Semester II","Semester III","Semester IV","Semester V","Semester VI","Semester VII"};
        
        // Act and Arrange section

        foreach (string test in testingSemesterNames)
        {
            // Inner Act section
            testSemester.NameProperty = test;
            
            // Inner Assert Section
            Assert.AreEqual(test,testSemester.NameProperty);
        }
    }

    [TestMethod]
    public void Name_Provided_Correctly_Random_ED()
    {
        // Testing random semester name
        // Test for Engineer Degree title
        // Arrange section
        string[] testingSemesterNames = new string[7] {"Semester I","Semester II","Semester III","Semester IV","Semester V","Semester VI","Semester VII"};
        Semester testSemester = new Semester("Master Engineer Degree");
        Random rndIndexGenerator = new Random();
        int rndIndex = rndIndexGenerator.Next(0, 7);
        
        // Act Section
        string testingName = testingSemesterNames[rndIndex];
        testSemester.NameProperty = testingName;
        
        // Assert Section
        Assert.AreEqual(testingName,testSemester.NameProperty);

    }
    [TestMethod]
    public void Name_Provided_Wrong_ED_Title()
    {
        // Testing protection against providing wrong semester name to Semester object
        // Test for Engineer Degree title
        // nameProperty should raise ArgumentException
        
        // Arrange section
        Semester testSemester = new Semester("Engineer Degree");
        string testName = "WRONG SEMESTER";
        
        // Act and Assert section
        Assert.ThrowsException<ArgumentException>(() => testSemester.NameProperty = testName);
    }

    [TestMethod]
    public void Name_Provided_Correctly_Random_MED()
    {
        // Testing random name for Master Engineer Degree title
        
        // Arrange Section
        Semester testSemester = new Semester("Master Engineer Degree");
        string[] testingSemesterNames = new string[3] {"Semester I","Semester II","Semester III"};
        Random rndIndexGenerator = new Random();
        int rndIndex;
        
        // Act Section
        rndIndex = rndIndexGenerator.Next(0, 2);
        string generatedName = testingSemesterNames[rndIndex];
        testSemester.NameProperty = generatedName;
        
        // Assert Section
        Assert.AreEqual(generatedName,testSemester.NameProperty);
    }

    [TestMethod]
    public void Name_Provided_Correctly_MED_Title()
    {
        // Testing name set and get property
        // Testing all names available for Master Engineer Degree (MED) title
        
        // Arrange Section
        Semester testSemester = new Semester("Master Engineer Degree");
        string[] testingSemesterNames = new string[3] {"Semester I","Semester II","Semester III"};
        
        // Act and Assert Section
        foreach (string name in testingSemesterNames)
        {
            // Inner Act Section
            testSemester.NameProperty = name;
            
            // Inner Assert Section
            Assert.AreEqual(name,testSemester.NameProperty);
        }
    }

    [TestMethod]
    public void Name_Provided_Wrong_MED_Title()
    {
        // Testing protection against providing wrong semester name to Semester object
        // Test for Master Engineer Degree title
        // nameProperty should raise ArgumentException
        
        // Arrange section
        Semester testSemester = new Semester("Master Engineer Degree");
        string wrongName = "WRONG NAME";
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testSemester.NameProperty = wrongName);
    }

    [TestMethod]
    public void Provided_Wrong_Title()
    {
        // Testing protection against providing wrong title
        // Testing only nameProperty, object should not be created if wrong title is provided
        
        // Arrange Section
        Semester testSemester;
        string name = "Semestr I";
        
        // Act and Arrange Section 
        Assert.ThrowsException<ArgumentException>(() => testSemester = new Semester("WRONG"));
    }
}

[TestClass]
public class EctsTest
{
    // Testing ectsTotalProperty in Semester class
    // Test works for title ED and MED
    [TestMethod]
    public void Ects_Provided_Correctly()
    {
        // Testing set and get property
        
        // Arrange Section
        Semester testSemester = new Semester("Engineer Degree");
        short ectsTest = 24;
        
        // Act Section
        testSemester.EctsTotalProperty = ectsTest;
        
        // Assert Section
        Assert.AreEqual(ectsTest,testSemester.EctsTotalProperty);
    }

    [TestMethod]
    public void Ects_Set_Negative()
    {
        // Testing protection against providing negative ects 
        // Property should raise Argument Exception
        
        // Arrange Section
        Semester testSemester = new Semester("Engineer Degree");
        short ectsTest = -28;
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testSemester.EctsTotalProperty = ectsTest);
    }

    [TestMethod]
    public void Ects_Set_Too_High()
    {
        // Testing protection against providing too high ects number (Over 40)    
        
        // Arrange Section
        Semester testSemester = new Semester("Engineer Degree");
        short ectsTest = 300;
        
        // Act and assert section

        Assert.ThrowsException<ArgumentException>(() => testSemester.EctsTotalProperty = ectsTest);
    }
}

[TestClass]
public class DatabaseTest
{
    // Testing semester class database methods like create, drop table and check if exists
    // Testing also data insertion and data fetching
    
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        // Initialize Test
        TestUtils.TableDrop();
        Field testField = new Field();
        testField.DropTable();
        testField.CreateTable();
        testField.DataInsertion();
        // https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2013/ms245265(v=vs.120)
    }

    // [TestInitialize]
    // public void TestInit()
    // {
    //     TestUtils.TableDrop();
    //     Field testField = new Field();
    //     testField.DropTable();
    //     testField.CreateTable();
    //     testField.DataInsertion();
    // }
    [TestMethod]
    public void Testing_Table_Creation()
    {
        // Testing CreateTable method from Semester Class 
        // Method should create table if not exists
        // Using TestUtils to drop table and check if new is created
        // We assume that TestUtils works 100% fine.
        
        // Arrange Section

        Semester testSemester = new Semester("ED", 1);

        // Act Section
        testSemester.CreateTable();
        
        // Assert Section
        Assert.AreEqual(true, TestUtils.TableExists());
    }
    
    
    [TestMethod]
    public void Testing_Table_Drop()
    {
        // Testing DropTable method from Semester class
        
        // Arrange Section
        TestUtils.TableDrop();
        TestUtils.CreateSimpleTableToDrop();
        Field testField = new Field();
        testField.DropTable();
        testField.CreateTable();
        testField.DataInsertion();
        Semester testSemester = new Semester("ED",1);
        
        // Act Section
        testSemester.DropTable();
        
        // Assert Section
        Assert.AreEqual(false,TestUtils.TableExists());
    }

    [TestMethod]
    public void Testing_Table_Data_Insertion()
    {
        // Testing data insertion method
        
        // Arrange Section
        Semester testSemester = new Semester("ED", 1);
        string testName = "Semester III";
        short testEcts = 23;
        int referenceTestId = 1;
        
        int fetchedId;
        string fetchedName;
        short fetchedEctsTotal;
        short fetchedEctsObtained;
        double fetchedAvereage;
        int fetchedReferenceId;

        // Act Section
        testSemester.CreateTable();
        testSemester.NameProperty = testName;
        testSemester.EctsTotalProperty = testEcts;
        testSemester.DataInsertion();
        
        // Fetching data from Database
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT * FROM semester;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                var result = command.ExecuteReader();
                result.Read();
                fetchedId = result.GetInt32(0);
                fetchedName = result.GetString(1);
                fetchedEctsTotal = result.GetInt16(2);
                fetchedEctsObtained = result.GetInt16(3);
                fetchedAvereage = result.GetDouble(4);
                fetchedReferenceId = result.GetInt32(5);
                result.Close();
            }
            connection.Close();
        }
        // Assert Section
        Assert.AreEqual(1,fetchedId);
        Assert.AreEqual(testName,fetchedName);
        Assert.AreEqual(testEcts,fetchedEctsTotal);
        Assert.AreEqual(0,fetchedEctsObtained);
        Assert.AreEqual(0,fetchedAvereage);
        Assert.AreEqual(referenceTestId,fetchedReferenceId);
        
    }

    [TestMethod]
    public void Testing_Table_Exists()
    {
        // Testing TableExists method
        // Table will exists so method should return True
        
        // Arrange Section
        Semester testSemester = new Semester("ED", 1);
        testSemester.CreateTable();
        bool result;
        
        // Act Section
        result = testSemester.TableExists();
        
        // Assert Section
        Assert.AreEqual(true,result);

    }

    [TestMethod]
    public void Testing_Table_Not_Exists()
    {
        // Testing TableExists method
        // Table won't exists so method should return False
        
        // Arrange Section
        Semester testSemester = new Semester("ED",1);
        bool result;
        // Act Section
        result = testSemester.TableExists();
        
        // Assert Section
        Assert.AreEqual(false,result);
    }
    
    [TestCleanup]
    public void AfterTestCleanUp()
    {
        TestUtils.TableDrop();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        // Cleaning after tests
        Console.WriteLine("Cleanup");
        Field testField = new Field();
        testField.DropTable();
    }
    
    
}