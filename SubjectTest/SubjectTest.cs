using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using University;

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
        testSubject.nameProperty = name;
        
        // Assert Section
        Assert.AreEqual(name,testSubject.nameProperty);
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
        Assert.ThrowsException<ArgumentException>(() => testSubject.nameProperty = name);
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
        Assert.ThrowsException<ArgumentException>(() => testSubject.nameProperty = name);
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
        testSubject.examGradeProperty = grade;
        testSubject.endingGradeProperty = grade;
        testSubject.exceriseGradeProperty = grade;
        testSubject.laboratoryGradeProperty = grade;
        
        // Assert Section
        Assert.AreEqual(grade,testSubject.examGradeProperty);
        Assert.AreEqual(grade,testSubject.endingGradeProperty);
        Assert.AreEqual(grade,testSubject.exceriseGradeProperty);
        Assert.AreEqual(grade,testSubject.laboratoryGradeProperty);
    }
    [TestMethod]
    public void Grade_Provided_Not_Correctly()
    {
        // Testing set and get proeperty
        
        // Arrange Section
        Subject testSubject = new Subject();
        float grade = 4.2f;
        
        // Act and Assert Section
        Assert.ThrowsException<ArgumentException>(() => testSubject.endingGradeProperty = grade);
        Assert.ThrowsException<ArgumentException>(() => testSubject.exceriseGradeProperty = grade);
        Assert.ThrowsException<ArgumentException>(() => testSubject.examGradeProperty = grade);
        Assert.ThrowsException<ArgumentException>(() => testSubject.laboratoryGradeProperty = grade);
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
        double average = 0.0;
        
        // Act Section
        testSubject.laboratoryGradeProperty = firstGrade;
        testSubject.examGradeProperty = secondGrade;
        testSubject.exceriseGradeProperty = thirdGrade;
        average = Math.Round((firstGrade + secondGrade + thirdGrade) / 3, 3);
        testSubject.AverageCalculate();
        
        // Assert Section
        Assert.AreEqual((float)average,testSubject.averageProperty);
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
        testSubject.examGradeProperty = gradeOne;
        testSubject.laboratoryGradeProperty = gradeTwo;
        testSubject.AverageCalculate();
        float expected = (float) Math.Round((gradeOne + gradeTwo) / 2, 3);
        // Assert Section
        Assert.AreEqual(expected,testSubject.averageProperty);

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
        testSubject.examGradeProperty = avg;
        testSubject.AverageCalculate();
        
        // Assert Section
        Assert.AreEqual(avg,testSubject.averageProperty);
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
        Assert.AreEqual(expected,testSubject.averageProperty);

    }
    [TestMethod]
    public void Average_Random_Grades()
    {
        // Testing Random input average calculate
        
        // Arrange Section
        int index = 0;
        int n = 0;
        float result = 0.0f;
        float sum = 0.0f;
        
        Random gen = new Random();
        float[] grades = new[] {0.0f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f};
        Subject testSubject = new Subject();
        
        Action<float> AvgStep = f =>
        {
            if (f!=0.0)
            {
                n++;
                sum += f;
            }
        }; 

        
        // Act Section
        
        index = gen.Next(0, 8);
        testSubject.examGradeProperty = grades[index];
        AvgStep(grades[index]);
        
        index = gen.Next(0, 8);
        testSubject.laboratoryGradeProperty = grades[index];
        AvgStep(grades[index]);
        
        index = gen.Next(0, 8);
        testSubject.exceriseGradeProperty = grades[index];
        AvgStep(grades[index]);
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
        Assert.AreEqual(result,testSubject.averageProperty);
    }
}