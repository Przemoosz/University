using System;
using System.IO;
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
        testField.NameProperty = nameToSet;
        
        // Assert Section
        Assert.AreEqual(nameToSet,testField.NameProperty);
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
        Assert.ThrowsException<ArgumentException>(() => testField.NameProperty = nameToSet);
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
        Assert.ThrowsException<ArgumentException>(() => testField.NameProperty = testName);
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
        testField.EctsTotalProperty = ectsTest;
        
        // Assert Section
        Assert.AreEqual(ectsTest, testField.EctsTotalProperty);
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
        Assert.ThrowsException<ArgumentException>(() => testField.EctsTotalProperty = ectsTest);
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
        Assert.ThrowsException<ArgumentException>(() => testField.EctsTotalProperty = ectstest);
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
        testField.YearStartingProperty = yearTest;
        
        // Assert Section 
        Assert.AreEqual(yearTest,testField.YearStartingProperty);
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
        Assert.ThrowsException<ArgumentException>(() => testField.YearStartingProperty = minYear);
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
        Assert.ThrowsException<ArgumentException>(() => testField.YearStartingProperty = maxYear);
    }

    [TestMethod]
    public void YearEnding_Provided_Correctly()
    {
        // Testing if StartingYearProperty get and set works properly 
        
        // Arrange Section
        short testEndingYear = 2025;
        Field testField = new Field();
        testField.YearStartingProperty = 2021;
        
        // Act Section
        testField.YearEndingProperty = testEndingYear;
        
        // Assert Section
        Assert.AreEqual(testEndingYear,testField.YearEndingProperty);
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
        testField.YearStartingProperty = testStartingYear;
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.YearEndingProperty = testingEndingYear);
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
        testField.YearStartingProperty = testStartingYear;
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.YearEndingProperty = testEndingYear);
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
        testField.TitleProperty = testTitle;
        
        // Assert Section
        Assert.AreEqual(testTitle,testField.TitleProperty);
        
    }
    [TestMethod]
    public void Title_MED_got_correctly()
    {
        // Testing set and get method if provide method return "Master Engineer Degree"
        
        // Arrange Section
        Field testField = new Field();
        string testTitle = "Master Engineer Degree";
        
        // Act Section
        testField.TitleProperty = testTitle;
        
        // Assert Section
        Assert.AreEqual(testTitle,testField.TitleProperty);
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
        Assert.ThrowsException<ArgumentException>(() => testField.TitleProperty = testTitle);
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
                command.CommandText = "DROP TABLE IF EXISTS fields CASCADE;";
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
        Assert.AreEqual(true, exists);
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
        Assert.AreEqual(false, result);
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
        Assert.AreEqual(true, result);


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
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Testing_Data_Insertion()
    {
        // Inserting specified data, and testing if DataInsertion method
        // inserted value in correct way.

        // Arrange Section
        Field testingField = new Field();
        string testName = "TestName";
        string testTitle = "Engineer Degree";
        short testEctsTotal = 43;
        short testStartingYear = 2021;
        short testEndingYear = 2024;

        int id;
        string nameFetched;
        short ectsTotalFetched;
        short ectsObtainedFetched;
        short startingYearFetched;
        short endingYearFetched;
        string titleFetched;
        TestUtils.TableDrop();

        // Act Section
        testingField.CreateTable();
        testingField.NameProperty = testName;
        testingField.TitleProperty = testTitle;
        testingField.EctsTotalProperty = testEctsTotal;
        testingField.YearStartingProperty = testStartingYear;
        testingField.YearEndingProperty = testEndingYear;
        testingField.DataInsertion();

        // Fetching data from DB
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string command = "SELECT * FROM fields;";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, connection))
            {
                var resultObject = cmd.ExecuteReader();
                resultObject.Read();
                id = resultObject.GetInt32(0);
                nameFetched = resultObject.GetString(1);
                ectsTotalFetched = resultObject.GetInt16(2);
                ectsObtainedFetched = resultObject.GetInt16(3);
                startingYearFetched = resultObject.GetInt16(4);
                endingYearFetched = resultObject.GetInt16(5);
                titleFetched = resultObject.GetString(6);
                resultObject.Close();
            }

            connection.Close();
        }

        // Assert Section
        Assert.AreEqual(1, id);
        Assert.AreEqual(testName, nameFetched);
        Assert.AreEqual(testEctsTotal, ectsTotalFetched);
        Assert.AreEqual(0, ectsObtainedFetched);
        Assert.AreEqual(testStartingYear, startingYearFetched);
        Assert.AreEqual(testEndingYear, endingYearFetched);
        Assert.AreEqual(testTitle, titleFetched);
        TestUtils.TableDrop();
    }

    [TestMethod]
    public void Testing_Load_Data_From_Database()
    {
        // Testing load data method from field class
        // Method should load all information about field from DB

        // Arrange Section
        Field testField = new Field();
        testField.TitleProperty = "Engineer Degree";
        testField.DropTable();
        testField.CreateTable();
        testField.DataInsertion();
        Field testResultField = new Field();

        using (var sr = new StringReader("1"))
        {

            Console.SetIn(sr);

            // Act Section
            testResultField.LoadFieldFromDatabase();
            
            // Assert Section
            Assert.AreEqual(1,testResultField.FieldIdProperty);
            Assert.AreEqual("None",testResultField.NameProperty);
            Assert.AreEqual(0,testResultField.EctsObtainedProperty);
            Assert.AreEqual(0,testResultField.EctsTotalProperty);
            Assert.AreEqual(Convert.ToInt16(DateTime.Now.Year),testResultField.YearStartingProperty);
            Assert.AreEqual(Convert.ToInt16(DateTime.Now.Year+3),testResultField.YearEndingProperty);
            Assert.AreEqual("Engineer Degree",testResultField.TitleProperty);
        }
        
    }
}

[TestClass]
public class ProvidingMethodsTests
{
    // Testing all provide methods from fields class
    // Setting too long/blank/too short variables is not possible due to 
    // protection inside Property Setters
    
    [TestMethod]
    public void Name_Providing_Test()
    {
        // Testing name providing method
        // Method should save provided name in name variable
        
        // Arrange Section
        Field testField = new Field();
        string name = "TestName";
        
        // Act Section
        using (var sr = new StringReader(name))
        {
            Console.SetIn(sr);
            testField.ProvideName();
        }
        
        // Assert Section
        Assert.AreEqual(name,testField.NameProperty);
    }

    [TestMethod]
    public void Ects_Total_Providing_Test()
    {
        // Testing Ects providing method
        // Method should save provided ects total number in ects total variable
        
        // Assert Section
        Field testField = new Field();
        string ects = "20";
        
        // Act Section
        using (var sr = new StringReader(ects))
        {
            Console.SetIn(sr);
            testField.ProvideEcstTotal();
        }
        // Assert Section
        Assert.AreEqual(Int16.Parse(ects),testField.EctsTotalProperty);
    }

    [TestMethod]
    public void Year_Starting_Providing_Test()
    {
        // Testing Starting Year providing method
        // Method should save provided starting year in starting year variable
        
        // Arrange Section
        Field testField = new Field();
        string startingYear = "2020";
        
        // Act Section
        using (var sr = new StringReader(startingYear))
        {
            Console.SetIn(sr);
            testField.ProvideStartingYear();
        }
        
        // Assert Section
        Assert.AreEqual(Int16.Parse(startingYear),testField.YearStartingProperty);
    }
    
    [TestMethod]
    public void Year_Ending_Providing_Test()
    {
        // Testing Starting Year providing method
        // Method should save provided ending year in ending year variable
        
        // Arrange Section
        Field testField = new Field();
        testField.YearStartingProperty = 2020;
        string endingYear = "2023";
        
        // Act Section
        using (var sr = new StringReader(endingYear))
        {
            Console.SetIn(sr);
            testField.ProvideStartingYear();
        }
        
        // Assert Section
        Assert.AreEqual(Int16.Parse(endingYear),testField.YearStartingProperty);
    }

    [TestMethod]
    public void Title_Providing_Method()
    {
        // Testing Title providing method
        // Method should save provided title in correct form in title variable
        
        // Arrange Section
        Field testField = new Field();
        string choosedNumber = "1";
        
        // Act Section
        using (var sr = new StringReader(choosedNumber))
        {
            Console.SetIn(sr);
            testField.ProvideTitle();
        }
        
        // Assert Section
        Assert.AreEqual("Engineer Degree", testField.TitleProperty);
    }
}