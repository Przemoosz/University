using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        Semester testSemester = new Semester("ED");
        string[] testingSemesterNames = new string[7] {"Semester I","Semester II","Semester III","Semester IV","Semester V","Semester VI","Semester VII"};
        
        // Act and Arrange section

        foreach (string test in testingSemesterNames)
        {
            // Inner Act section
            testSemester.nameProperty = test;
            
            // Inner Assert Section
            Assert.AreEqual(test,testSemester.nameProperty);
        }
    }

    [TestMethod]
    public void Name_Provided_Correctly_Random_ED()
    {
        // Testing random semester name
        // Test for Engineer Degree title
        // Arrange section
        string[] testingSemesterNames = new string[7] {"Semester I","Semester II","Semester III","Semester IV","Semester V","Semester VI","Semester VII"};
        Semester testSemester = new Semester("ED");
        Random rndIndexGenerator = new Random();
        int rndIndex = rndIndexGenerator.Next(0, 8);
        
        // Act Section
        string testingName = testingSemesterNames[rndIndex];
        testSemester.nameProperty = testingName;
        
        // Assert Section
        Assert.AreEqual(testingName,testSemester.nameProperty);

    }
    [TestMethod]
    public void Name_Provided_Wrong_ED_Titile()
    {
        // Testing protection against providing wrong semester name to Semester object
        // Test for Engineer Degree title
        // nameProperty should raise ArgumentException
        
        // Arrange section
        Semester testSemester = new Semester("ED");
        string testName = "WRONG SEMESTER";
        
        // Act and Assert section
        Assert.ThrowsException<ArgumentException>(() => testSemester.nameProperty = testName);
    }

    [TestMethod]
    public void Name_Provided_Correctly_Random_MED()
    {
        // Testing random name for Master Engineer Degree title
        
        // Arrange Section
        Semester testSemester = new Semester("MED");
        string[] testingSemesterNames = new string[3] {"Semester I","Semester II","Semester III"};
        Random rndIndexGenerator = new Random();
        int rndIndex;
        
        // Act Section
        rndIndex = rndIndexGenerator.Next(0, 3);
        string generatedName = testingSemesterNames[rndIndex];
        testSemester.nameProperty = generatedName;
        
        // Assert Section
        Assert.AreEqual(generatedName,testSemester.nameProperty);
    }

    [TestMethod]
    public void Name_Provided_Correctly_MED_Title()
    {
        // Testing name set and get property
        // Testing all names available for Master Engineer Degree (MED) title
        
        // Arrange Section
        Semester testSemester = new Semester("MED");
        string[] testingSemesterNames = new string[3] {"Semester I","Semester II","Semester III"};
        
        // Act and Assert Section
        foreach (string name in testingSemesterNames)
        {
            // Inner Act Section
            testSemester.nameProperty = name;
            
            // Inner Assert Section
            Assert.AreEqual(name,testSemester.nameProperty);
        }
    }

    [TestMethod]
    public void Name_Provided_Wrong_MED_Title()
    {
        // Testing protection against providing wrong semester name to Semester object
        // Test for Master Engineer Degree title
        // nameProperty should raise ArgumentException
        
        // Arrange section
        Semester testSemester = new Semester("MED");
        string wrongName = "WRONG NAME";
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testSemester.nameProperty = wrongName);
    }

    [TestMethod]
    public void Provided_Wrong_Title()
    {
        // Testing protection against providing wrong title
        // Testing only nameProperty, object should not be created if wrong title is provided
        
        // Arrange Section
        Semester testSemester = new Semester("WRONG");
        string name = "Semestr I";
        
        // Act and Arrange Section 
        Assert.ThrowsException<ArgumentException>(() => testSemester.nameProperty = name);
    }
}