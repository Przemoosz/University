using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Npgsql;
using NpgsqlTypes;

namespace University;


public class Field : IData
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
            else yearEnding = value;
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

    public Field()
    {
        //CreateTable();
    }
    public void CreateField()
    {
        
        ProvideName();
        ProvideEcstTotal();
        ProvideStartingYear();
        ProvideEndingYear();
        ProvideTitle();
        DataInsertion();
    }

    public void ProvideName()
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

    public void CreateTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd = @"CREATE TABLE IF NOT EXISTS fields
            (id SERIAL PRIMARY KEY,
                name VARCHAR(255) NOT NULL,
                ectsTotal SMALLINT NOT NULL,
                ectsObtained SMALLINT DEFAULT 0,
                startingYear SMALLINT NOT NULL,
                endingYear SMALLINT NOT NULL CHECK( endingYear <= startingYear+7 AND endingYear>startingYear),
                title VARCHAR(255) NOT NULL
            );";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd,connection))
            {
                command.ExecuteScalar();
            }
            connection.Close();
        }
    }

    public bool TableExists()
    {
        bool ret;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT EXISTS(SELECT FROM pg_tables WHERE schemaname='public' AND tablename='fields')";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                var result = command.ExecuteScalar();
                string res = result.ToString();
                ret = Convert.ToBoolean(res);
                Console.WriteLine(ret);
            }
            connection.Close();
        }

        return ret;
    }
    public void DropTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "DROP TABLE IF EXISTS fields CASCADE;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd,connection))
            {
                command.ExecuteScalar();
            }
            connection.Close();
        }
    }

    public void DataInsertion()
    {
        if (!TableExists()) CreateTable();
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "INSERT INTO fields VALUES(DEFAULT ,@Name, @EctsTotal, DEFAULT, @StartingYear, @EndingYear,@Title);";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                command.Parameters.Add("@Name", NpgsqlDbType.Varchar);
                command.Parameters.Add("@EctsTotal",NpgsqlDbType.Smallint);
                command.Parameters.Add("@StartingYear", NpgsqlDbType.Smallint);
                command.Parameters.Add("@EndingYear", NpgsqlDbType.Smallint);
                command.Parameters.Add("@Title", NpgsqlDbType.Varchar);
                command.Parameters["@Name"].Value = nameProperty;
                command.Parameters["@EctsTotal"].Value = ectsTotalProperty;
                command.Parameters["@StartingYear"].Value = yearStartingProperty;
                command.Parameters["@EndingYear"].Value = yearEndingProperty;
                command.Parameters["@Title"].Value = titleProperty;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex);
                    throw;
                }
            }
            connection.Close();
        }
    }

    public static void AllFieldsInDatabase()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT * FROM fields;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                var result = command.ExecuteReader();
                // Console.WriteLine(result.Re);
                Console.WriteLine("ID -- Name -- EctsTotal -- EctsObtained -- StartingYear -- EndingYear -- Title");
                foreach (var _ in result)
                {
                    Console.WriteLine($"{result.GetInt32(0)} -- {result.GetString(1)} -- {result.GetInt16(2)} -- {result.GetInt16(3)} -- {result.GetInt16(4)} -- {result.GetInt16(5)} -- {result.GetString(6)}");
                }
            }
            connection.Close();
        }
    }
    
}