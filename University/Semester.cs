using Npgsql;
using NpgsqlTypes;
namespace University;

public class Semester: IData
{
    private string _name  = "None";
    private short _ectsTotal = 0;
    private short _ectsObtained = 0;
    private float _average = 0.0f;
    private string _title;
    private int _fieldIdReference;
    private int _semesterId;
    public Semester(string title)
    {
        _title = title;
    }

    public Semester(string title, int fieldInt)
    {
        _title = title;
        _fieldIdReference = fieldInt;
    }
    public string nameProperty
    { 
        get => _name;
        set
        {
            if (_title == "ED")
            {
                if (value == "Semester I" || value == "Semester II" || value == "Semester III"
                    || value == "Semester IV" || value == "Semester V" || value == "Semester VI"
                    || value == "Semester VII")
                {
                    _name = value;
                }
                else
                {
                    // Console.WriteLine("Here!!1");
                    throw new ArgumentException(
                        "Method provided wrong value! This is unexpected! Contact developer, if this is repeating problem");
                }
            }
            else if (_title == "MED")
            {
                if (value == "Semester I" || value == "Semester II" || value == "Semester III")
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Method provided wrong value! This is unexpected! Contact developer, if this is repeating problem");
                }
            }
            else throw new ArgumentException("Title is not provided!");
        } 
    }

    public short ectsTotalProperty
    {
        get => _ectsTotal;
        set
        {
            if (value > 40) throw new ArgumentException("Total ects for semester should not be greater than 50!");
            if (value < 0) throw new ArgumentException("Total ects can not be negative number!");
            _ectsTotal = value;
        }
    }
    public void CreateTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string command = @"CREATE TABLE IF NOT EXISTS semester 
            (id SERIAL PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            ectsTotal SMALLINT NOT NULL,
            ectsObtained SMALLINT DEFAULT 0,
            average FLOAT DEFAULT 0,
            field_id INT REFERENCES fields(id))";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command,connection))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public void ProvideName()
    {
        string providedNumber;
        int number;
        
        if (_title == "ED")
        {
            Console.WriteLine($"Provide semester (From: 1 To: 7)");
            Console.Write("Selected Title (Number): ");
            providedNumber = Console.ReadLine().Trim();
            while (!Int32.TryParse(providedNumber, out number))
            {
                Console.WriteLine("Provided wrong number! Try again");
                Console.Write("Selected Title (Number): ");
                providedNumber = Console.ReadLine().Trim();
            }

            if (number > 7 || number <= 0)
            {
                throw new Exception("Provided wrong number. Numbers must higher than 0 and lower than 8!");
            }

            nameProperty = number switch
            {
                1 => "Semester I",
                2 => "Semester II",
                3 => "Semester III",
                4 => "Semester VI",
                5 => "Semester V",
                6 => "Semester VI",
                7 => "Semester VII",
                _ => "None"
            };
        }

        if (_title == "MED")
        {
            Console.WriteLine($"Provide semester (From: 1 To: 3)");
            Console.Write("Selected Title (Number): ");
            providedNumber = Console.ReadLine().Trim();
            while (!Int32.TryParse(providedNumber, out number))
            {
                Console.WriteLine("Provided wrong number! Try again");
                Console.Write("Selected Title (Number): ");
                providedNumber = Console.ReadLine().Trim();
            }
            if (number > 3 || number <= 0)
            {
                throw new Exception("Provided wrong number. Numbers must higher than 0 and lower than 4!");
            }

            nameProperty = number switch
            {
                1 => "Semester I",
                2 => "Semester II",
                3 => "Semester III",
                _ => "None"
            };
            
        }
        // Console.Write("Selected Title (Number): ");
    }

    public void ProvideEctsTotal()
    {
        string providedEctsString;
        short providedEcts; 
        Console.WriteLine("Total ECTS (Must be provided, not less than 0, not more than 40)");
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

    public void CreateSemester()
    {
        ProvideName();
        ProvideEctsTotal();
        DataInsertion();
    }

    public void DropTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "DROP TABLE IF EXISTS semester CASCADE";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public bool TableExists()
    {
        bool exists;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT EXISTS(SELECT FROM pg_tables WHERE schemaname='public' AND tablename='semester')";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                exists = Convert.ToBoolean(command.ExecuteScalar().ToString());
            connection.Close();
        }
        Console.WriteLine(exists);
        return exists;
    }

    public void DataInsertion()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string cmd =
                "INSERT INTO semester VALUES(DEFAULT, @Name, @EctsTotal, DEFAULT, DEFAULT, @IdReference) RETURNING id;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                command.Parameters.Add("@Name", NpgsqlDbType.Varchar);
                command.Parameters.Add("@EctsTotal", NpgsqlDbType.Smallint);
                command.Parameters.Add("@IdReference", NpgsqlDbType.Integer);
                command.Parameters["@Name"].Value = nameProperty;
                command.Parameters["@EctsTotal"].Value = ectsTotalProperty;
                command.Parameters["@IdReference"].Value = _fieldIdReference;
                try
                {
                    Object reader = command.ExecuteScalar();
                    _semesterId = Convert.ToInt32(reader.ToString());
                    Console.WriteLine(_semesterId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            connection.Close();
        }
    }

    public void FetchDataFromDb()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            
            string cmd = "SELECT * FROM semester;";
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                var result = command.ExecuteReader();
                Console.WriteLine("Id -- Name -- EctsTotal -- EctsObtained -- Average -- FieldReferenceID");
                foreach (var _ in result)
                {
                    Console.WriteLine($"{result.GetInt32(0)} -- {result.GetString(1)} -- {result.GetInt16(2)} -- {result.GetInt16(3)} -- {result.GetDouble(4)} -- {result.GetInt32(5)}");
                }
                result.Close();
            }

        }        
    }
}