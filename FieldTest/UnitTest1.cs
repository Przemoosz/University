using System;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using University;
using Npgsql;
namespace FieldTest;

[TestClass]
public class NameTest
{
    // Checking if setting name to Field object works properly,
    // name length should be bigger than 0 and not more than 255.
    // If name len is not between (0;255) - throws ArgumentException
    [TestMethod]
    public void Name_Set_Successful()
    {
        // Testing if name set and get works properly
        
        // Arrange section
        Field testField = new Field();
        string nameToSet = "TestField";
        
        // Act section
        testField.nameProperty = nameToSet;
        
        // Assert Section
        Assert.AreEqual(nameToSet,testField.nameProperty);
    }

    [TestMethod]
    public void Name_Set_Too_Long()
    {
        // Testing protection against providing name length higher than 255
        // Set property should raise Argument Exception
        
        // Arrange Section
        Field testField = new Field();
        string nameToSet =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam ullamcorper nisl justo, at efficitur nibh egestas eu. Donec dignissim et turpis sed tempus. Duis dapibus eleifend ullamcorper. Nulla lacus arcu, placerat eget molestie et, auctor ut lectus. Duis in tincidunt ex.";
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.nameProperty = nameToSet);
    }

    [TestMethod]
    public void Name_Set_Not_Provided()
    {
        // Testing protection against providing blank name (Length = 0)
        // Set property should raise Argument Exception
        
        // Arrange Section
        Field testField = new Field();
        string testName = "";
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.nameProperty = testName);
    }
    
}

[TestClass]
public class EctsTests
{
    // Testing if EctsProprety (Total/Obtained) Works fine
    // ects should be positive number, not greater than 200
    
    // TODO
    // ectsObtained Test
    
    [TestMethod]
    public void Ects_Provided_correctly()
    {
        // Testing if EctsTotal get and set works properly 
        
        // Arrange Section
        short ectsTest = 40;
        Field testField = new Field();
        
        // Act Section
        testField.ectsTotalProperty = ectsTest;
        
        // Assert Section
        Assert.AreEqual(ectsTest, testField.ectsTotalProperty);
    }

    [TestMethod]
    public void Ects_Too_High()
    {
        // Testing protection against providing ects number higher than 200
        // Set property should raise Argument Exception
        
        // Arrange Section
        short ectsTest = 300;
        Field testField = new Field();
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.ectsTotalProperty = ectsTest);
    }

    [TestMethod]
    public void Ects_Set_Negative()
    {
        // Testing protection against providing negative ects number
        // Set property should raise Argument Exception
        
        // Arrange Section
        short ectstest = -255;
        Field testField = new Field();
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.ectsTotalProperty = ectstest);
    }
}

[TestClass]
public class YearTest
{
    // Testing if YearProperty (Starting/Ending) works fine
    // Starting year should not be smaller than: current Year - 7
    // Starting year should not be greater than: current Year + 7
    // Ending Year should not be greater than: starting year + 7
    // Ending Year should not be smaller than: starting year
    [TestMethod]
    public void YearStarting_Provided_correctly()
    {
        // Testing if StartingYearProperty get and set works properly 
        
        // Arrange Section
        short yearTest= 2021;
        Field testField = new Field();
        
        // Act Section
        testField.yearStartingProperty = yearTest;
        
        // Assert Section 
        Assert.AreEqual(yearTest,testField.yearStartingProperty);
    }

    [TestMethod]
    public void YearStarting_Too_Low()
    {
        // Testing protection against providing year smaller than: current year - 7 
        // Property should raise Argument Exception
        
        // Arrange Section
        short minYear = Convert.ToInt16(DateTime.Now.Year - 8);
        Field testField = new Field();
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.yearStartingProperty = minYear);
    }

    [TestMethod]
    public void YearStarting_Too_High()
    {
        // Testing protection against providing year greater than: current year +7
        // Property should raise Argument Exception
        
        // Arrange Section
        short maxYear = Convert.ToInt16(DateTime.Now.Year + 8);
        Field testField = new Field();
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.yearStartingProperty = maxYear);
    }

    [TestMethod]
    public void YearEnding_Provided_Correctly()
    {
        // Testing if StartingYearProperty get and set works properly 
        
        // Arrange Section
        short testEndingYear = 2025;
        Field testField = new Field();
        testField.yearStartingProperty = 2021;
        
        // Act Section
        testField.yearEndingProperty = testEndingYear;
        
        // Assert Section
        Assert.AreEqual(testEndingYear,testField.yearEndingProperty);
    }

    [TestMethod]
    public void YearEnding_Lower_Than_YearStarting()
    {
        // Testing protection against providing endingYear lower than startingYear
        // Property should raise ArgumentException
        
        // Arrange Section
        short testStartingYear = 2021;
        short testingEndingYear = 1970;
        Field testField = new Field();
        testField.yearStartingProperty = testStartingYear;
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.yearEndingProperty = testingEndingYear);
    }

    [TestMethod]
    public void YearEnding_Greater_Than_Upper_Limit()
    {
        // Testing protection against providing endingYear greater than startingYear +7
        // Property should raise ArgumentException
        
        // Arrange Section
        short testStartingYear = 2021;
        short testEndingYear = 2029;
        Field testField = new Field();
        testField.yearStartingProperty = testStartingYear;
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.yearEndingProperty = testEndingYear);
    }
}

[TestClass]
public class TitleTests
{
    // Title set will be handled by other function which will be also tested
    // For now only 2 titles are available "Engineer Degree"(ED) and "Master Engineer Degree"(MED)
    // If providing method return something else Exception should be raised
    [TestMethod]
    public void Title_ED_got_correctly()
    {
        // Testing set and get method if provide method return "Engineer Degree"
        
        // Arrange Section
        Field testField = new Field();
        string testTitle = "Engineer Degree";
        
        // Act Section
        testField.titleProperty = testTitle;
        
        // Assert Section
        Assert.AreEqual(testTitle,testField.titleProperty);
        
    }
    [TestMethod]
    public void Title_MED_got_correctly()
    {
        // Testing set and get method if provide method return "Master Engineer Degree"
        
        // Arrange Section
        Field testField = new Field();
        string testTitle = "Master Engineer Degree";
        
        // Act Section
        testField.titleProperty = testTitle;
        
        // Assert Section
        Assert.AreEqual(testTitle,testField.titleProperty);
    }

    [TestMethod]
    public void Title_Provided_Not_Correctly()
    {
        // Testing protection if providing method returns different value than 2 fixed degree (MED and ED)
        // Property should raise ArgumentException
        
        // Arrange Section
        string testTitle = "Wrong Title";
        Field testField = new Field();
        
        // Act and Asser Section
        Assert.ThrowsException<ArgumentException>(() => testField.titleProperty = testTitle);
    }
}

[TestClass]
public class DatabaseTest
{
    // Testing tables in database
    // Field class should handle: CREATE, DROP, IF EXISTS method
    // Every test creates table first and after fetching result it drops table (or drops table before and after act section)
    // IMPORTANT NOTE: Running this test will drop whole table using CASCADE method (This includes other tables data)
    // After running test all your data will be lost!
    [TestMethod]
    public void Testing_Table_Creation()
    {
        // Testing CreateTable method 
        
        // Arrange Section
        Field testingField = new Field();
        bool exists;
        // Act Section
        testingField.CreateTable();
        using (NpgsqlCommand command = new NpgsqlCommand())
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection("Host=localhost;Username=test;Password=testpassword;Database=university;"))
            {
                connection.Open();
                command.Connection = connection;
                // First drop table
                command.CommandText = "DROP TABLE IF EXISTS fields;";
                command.ExecuteNonQuery();
                // Test CreateTable Method from Field Class
                testingField.CreateTable();
                // Fetch result if table exists
                command.CommandText =
                    "SELECT EXISTS(SELECT FROM pg_tables WHERE schemaname='public' AND tablename='fields')";
                exists = Convert.ToBoolean(command.ExecuteScalar().ToString());
                // Drop table
                command.CommandText = "DROP TABLE IF EXISTS fields;";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        // Assert Section
        Assert.AreEqual(true,exists);
    }

    [TestMethod]
    public void Testing_Table_Drop()
    {
        // Testing DropTable method
        
        // Arrange Section
        TestUtils.TableDrop();
        TestUtils.SimpleTableCreate();
        Field testField = new Field();
        bool result;
        
        // Act Section
        testField.DropTable();
        result = TestUtils.TableExists();
        
        // Assert Section
        Assert.AreEqual(false,result);
    }
    [TestMethod]
    public void Testing_Table_Exists()
    {
        // Testing TableExists method
        // Table fields will exist so method should return true
        
        // Arrange Section
        TestUtils.TableDrop();
        TestUtils.SimpleTableCreate();
        Field testField = new Field();
        bool result;
        
        // Act Section
        result = testField.TableExists();
        
        // Assert Section
        Assert.AreEqual(true,result);
        

    }
    [TestMethod]
    public void Testing_Table_Dont_Exists()
    {
        // Testing TableExist Method
        // Table "fields" won't exist so method should return false
        
        // Arrange Section
        TestUtils.TableDrop();
        Field testField = new Field();
        bool result;
        
        // Act Section
        result = testField.TableExists();
        
        // Assert Section
        Assert.AreEqual(false,result);
    }

    [TestMethod]
    public void Testing_Data_Insertion()
    {
        // Inserting specified data, and testing if DataInsertion method
        // inserted value in correct way.
        
    }
}