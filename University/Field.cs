using System.Diagnostics;

namespace University;

public class Field
{
    private string name = "None";
    private short ectsTotal = 0;
    private short ectsObtained = 0;
    private short yearStarting = Convert.ToInt16(DateTime.Now.Year);
    private short yearEnding = Convert.ToInt16(DateTime.Now.Year+3);
    private string title = "None";

    public string nameProperty
    {
        get => name;
        set
        {
            if (value.Length > 255) throw new ArgumentException("Name provided is too long", value);
            else if (value.Length == 0) throw new ArgumentException("Name is not provided!", value);
            else name = value;
        }
    }

    public short ectsTotalProperty
    {
        get => ectsTotal;
        set
        {
            if (value < 0 ) throw new ArgumentException("Ects Total can't be a negative number");
            else if (value > 200) throw new ArgumentException("Ects Total can't be higher than 200");
            else ectsTotal = value;
        }
    }
    public short ectsObtainedProperty
    {
        get => ectsTotal;
        set
        {
            // ECTS Obtained is sum from ECTS semesters
            // WIP
        }
    }

    public short yearStartingProperty
    {
        get => yearStarting;
        set
        {
            if (value < DateTime.Now.Year - 7) throw new ArgumentException($"Starting year should not be lower than {DateTime.Now.Year - 7}");
            else if (value > DateTime.Now.Year + 7)
                throw new ArgumentException($"Starting year should not be greater than {DateTime.Now.Year - 7}");
            else yearStarting = value;
        }
    }

    public short yearEndingProperty
    {
        get => yearEnding;
        set
        {
            if (value < yearStarting) throw new ArgumentException($"University ending year should not be lower than {yearStarting}");
            else if (value > yearStarting + 7)
                throw new ArgumentException($"University ending year should not be greater than {yearStarting +7}");
            else yearStarting = value;
        }
    }

    public string titleProperty
    {
        get => title;
        set
        {
            if (value != "Engineer Degree" && value != "Master Engineer Degree")
                throw new ArgumentException("Method provided wrong value! This is unexpected! Contact developer, if this is repeating problem");
            else title = value;
        }
    }

    public void CreateField()
    {
        ProvideName();
        ProvideEcstTotal();
        ProvideStartingYear();
        ProvideEndingYear();
        ProvideTitle();
    }      

    private void ProvideName()
    {
        string providedName; 
        Console.WriteLine("Field name (Must be provided, not longer than 255 letters)");
        Console.Write("Your field name: ");
        providedName = Console.ReadLine().Trim();
        nameProperty = providedName;
    }

    private void ProvideEcstTotal()
    {
        string providedEctsString;
        short providedEcts; 
        Console.WriteLine("Total ECTS (Must be provided, not less than 0, not more than 200)");
        Console.Write("Your Total ECTS: ");
        providedEctsString = Console.ReadLine().Trim();
        while (!Int16.TryParse(providedEctsString, out providedEcts))
        {
            Console.WriteLine("Provided wrong number! Try again.");
            Console.Write("Your Total ECTS: ");
            providedEctsString = Console.ReadLine().Trim();
        }
        ectsTotalProperty = providedEcts;
    }

    private void ProvideStartingYear()
    {
        string providedStartingYearString;
        short providedStartingYear; 
        Console.WriteLine($"Starting year (Must be provided, not lower than {DateTime.Now.Year-7}, not greater than than {DateTime.Now.Year+7})");
        Console.Write("Your starting year: ");
        providedStartingYearString = Console.ReadLine().Trim();
        while (!Int16.TryParse(providedStartingYearString, out providedStartingYear))
        {
            Console.WriteLine("Provided wrong number! Try again.");
            Console.Write("Your Total ECTS: ");
            providedStartingYearString = Console.ReadLine().Trim();
        }
        yearStartingProperty = providedStartingYear;
    }

    private void ProvideEndingYear()
    {
        string providedEndingYearString;
        short providedEndingYear; 
        Console.WriteLine($"Ending year (Must be provided, not lower than {yearStartingProperty}, not greater than than {yearStartingProperty+7})");
        Console.Write("Your ending year: ");
        providedEndingYearString = Console.ReadLine().Trim();
        while (!Int16.TryParse(providedEndingYearString, out providedEndingYear))
        {
            Console.WriteLine("Provided wrong number! Try again.");
            Console.Write("Your Total ECTS: ");
            providedEndingYearString = Console.ReadLine().Trim();
        }
        yearEndingProperty = providedEndingYear;
    }

    private void ProvideTitle()
    {
        string providedNumber;
        int num;
        string providedTitle; 
        Console.WriteLine($"Select Title(1 - Engineer Degree, 2 - Master Engineer Degree)");
        Console.Write("Selected Title (Number): ");
        providedNumber = Console.ReadLine().Trim();
        while (!Int32.TryParse(providedNumber, out num))
        {
            Console.WriteLine("Provided wrong number! Try again.");
            Console.Write("Your Total ECTS: ");
            providedNumber = Console.ReadLine().Trim();
        }

        titleProperty = num switch
        {
            1 => "Engineer Degree",
            2 => "Master Engineer Degree",
            _ => "None"
        };
    }
}