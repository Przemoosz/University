using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using University;
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
    // Starting year should not be smaller thank: current Year - 7
    // Ending Year should not be greater than current Year + 7
    [TestMethod]
    public void YearStarting_Provided_correctly()
    {
        // Testing if EctsTotal get and set works properly 
        
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
        short minYear = Convert.ToInt16(DateTime.Now.Year - 7);
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
        short maxYear = Convert.ToInt16(DateTime.Now.Year + 7);
        Field testField = new Field();
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testField.yearStartingProperty = maxYear);
    }
}