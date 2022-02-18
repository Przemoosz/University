using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Npgsql;
using NpgsqlTypes;

namespace University;


public class Field : IData
{
    private string _name = "None";
    private short _ectsTotal = 0;
    private short _ectsObtained = 0;
    private short _yearStarting = Convert.ToInt16(DateTime.Now.Year);
    private short _yearEnding = Convert.ToInt16(DateTime.Now.Year+3);
    private string _title = "None";
    // Default seted to one
    private int _fieldId = 1;

    public string NameProperty
    {
        get => _name;
        set
        {
            if (value.Length > 255) throw new ArgumentException("Name provided is too long", value);
            else if (value.Length == 0) throw new ArgumentException("Name is not provided!", value);
            else _name = value;
        }
    }

    public short EctsTotalProperty
    {
        get => _ectsTotal;
        set
        {
            if (value < 0 ) throw new ArgumentException("Ects Total can't be a negative number");
            else if (value > 200) throw new ArgumentException("Ects Total can't be higher than 200");
            else _ectsTotal = value;
        }
    }
    public short EctsObtainedProperty
    {
        get => _ectsTotal;
        set
        {
            // ECTS Obtained is sum from ECTS semesters
            // WIP
            _ectsTotal = value;
        }
    }

    public short YearStartingProperty
    {
        get => _yearStarting;
        set
        {
            if (value < DateTime.Now.Year - 7) throw new ArgumentException($"Starting year should not be lower than {DateTime.Now.Year - 7}");
            else if (value > DateTime.Now.Year + 7)
                throw new ArgumentException($"Starting year should not be greater than {DateTime.Now.Year - 7}");
            else _yearStarting = value;
        }
    }

    public short YearEndingProperty
    {
        get => _yearEnding;
        set
        {
            if (value < _yearStarting) throw new ArgumentException($"University ending year should not be lower than {_yearStarting}");
            else if (value > _yearStarting + 7)
                throw new ArgumentException($"University ending year should not be greater than {_yearStarting +7}");
            else _yearEnding = value;
        }
    }

    public string TitleProperty
    {
        get => _title;
        set
        {
            if (value != "Engineer Degree" && value != "Master Engineer Degree")
                throw new ArgumentException("Method provided wrong value! This is unexpected! Contact developer, if this is repeating problem");
            else _title = value;
        }
    }

    public int FieldIdProperty
    {
        set => _fieldId = value;
        get => _fieldId;
    }
    public Field()
    {

    }
    public void CreateField()
    {
        if (!TableExists())
        {
            CreateTable();
        }
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
        NameProperty = providedName;
    }

    public void ProvideEcstTotal()
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
        EctsTotalProperty = providedEcts;
    }

    public void ProvideStartingYear()
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
        YearStartingProperty = providedStartingYear;
    }

    public void ProvideEndingYear()
    {
        string providedEndingYearString;
        short providedEndingYear; 
        Console.WriteLine($"Ending year (Must be provided, not lower than {YearStartingProperty}, not greater than than {YearStartingProperty+7})");
        Console.Write("Your ending year: ");
        providedEndingYearString = Console.ReadLine().Trim();
        while (!Int16.TryParse(providedEndingYearString, out providedEndingYear))
        {
            Console.WriteLine("Provided wrong number! Try again.");
            Console.Write("Your Total ECTS: ");
            providedEndingYearString = Console.ReadLine().Trim();
        }
        YearEndingProperty = providedEndingYear;
    }

    public void ProvideTitle()
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

        TitleProperty = num switch
        {
            1 => "Engineer Degree",
            2 => "Master Engineer Degree",
            _ => "None"
        };
    }

    public void CreateTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT EXISTS(SELECT FROM pg_tables WHERE schemaname='public' AND tablename='fields')";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                var result = command.ExecuteScalar();
                string res = result.ToString();
                ret = Convert.ToBoolean(res);
                // Console.WriteLine(ret);
            }
            connection.Close();
        }

        return ret;
    }
    public static bool TableExistsStatic()
    {
        bool ret;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT EXISTS(SELECT FROM pg_tables WHERE schemaname='public' AND tablename='fields')";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                var result = command.ExecuteScalar();
                string res = result.ToString();
                ret = Convert.ToBoolean(res);
                // Console.WriteLine(ret);
            }
            connection.Close();
        }

        return ret;
    }
    public void DropTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
        string cmd = "INSERT INTO fields VALUES(DEFAULT ,@Name, @EctsTotal, DEFAULT, @StartingYear, @EndingYear,@Title) RETURNING id;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                command.Parameters.Add("@Name", NpgsqlDbType.Varchar);
                command.Parameters.Add("@EctsTotal",NpgsqlDbType.Smallint);
                command.Parameters.Add("@StartingYear", NpgsqlDbType.Smallint);
                command.Parameters.Add("@EndingYear", NpgsqlDbType.Smallint);
                command.Parameters.Add("@Title", NpgsqlDbType.Varchar);
                command.Parameters["@Name"].Value = NameProperty;
                command.Parameters["@EctsTotal"].Value = EctsTotalProperty;
                command.Parameters["@StartingYear"].Value = YearStartingProperty;
                command.Parameters["@EndingYear"].Value = YearEndingProperty;
                command.Parameters["@Title"].Value = TitleProperty;
                try
                {
                    Object reader = command.ExecuteScalar();
                    _fieldId = Convert.ToInt32(reader);
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

    public static int AllFieldsInDatabase()
    {
        int rows = 0;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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
                    rows = result.GetInt32(0);
                }

                
            }
            connection.Close();
        }

        return rows;
    }

    public void ShowConnectedSemesters()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT fields.id, fields.name, semester.name AS semname,semester.ectstotal, semester.ectsobtained, average FROM fields INNER JOIN semester ON fields.id = field_id;";
            using (NpgsqlCommand commmand = new NpgsqlCommand(cmd,connection))
            {
                NpgsqlDataReader reader = commmand.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("ID -- FieldName -- SemName -- EctsTotal -- EctsObtained -- Average");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetInt32(0)} -- {reader.GetString(1)} -- {reader.GetString(2)} -- {reader.GetInt16(3)} -- " +
                                          $"{reader.GetInt16(4)} -- {reader.GetDouble(5)}");
                    }
                }
                else
                {
                    //TODO
                }
            }
            connection.Close();
        }
    }

    public void ObtainedEctsRecalculate()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            short newEctsTotal = 0;
            short newEctsObtained = 0;
            connection.Open();
            // Console.WriteLine(_fieldId);
            string cmd = $"SELECT fields.id, semester.ectsobtained,semester.ectstotal FROM fields INNER JOIN semester ON semester.field_id = {this._fieldId};";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // Console.WriteLine("Name -- EctsObtained -- EctsTotal -- Average");
                    while (reader.Read())
                    {
                        newEctsObtained += reader.GetInt16(1);
                        newEctsTotal += reader.GetInt16(2);
                    }
                }
                else
                {
                    Console.WriteLine("No rows Available");
                }
                reader.Close();
            }

            cmd = $"UPDATE fields SET ectsobtained = {newEctsObtained}, ectstotal = {newEctsTotal} WHERE id = {this._fieldId}";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                Console.WriteLine(newEctsTotal);
                command.ExecuteNonQuery();
            }

            _ectsObtained = newEctsObtained;
            _ectsTotal = newEctsTotal;
                connection.Close();
        }
    }

    public void LoadFieldFromDatabase()
    {
        int chosedNumber;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            string cmd = "SELECT id,name from fields;";
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("Available fields to load: ");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetInt32(0)} -- {reader.GetString(1)}");
                    }
                }
                else
                {
                    Console.WriteLine("There is no rows available!");
                }
                reader.Close();
            }
            Console.WriteLine("Choose field (type number): ");
            chosedNumber = Int32.Parse(Console.ReadLine());
            Console.WriteLine(chosedNumber);
            cmd = $"SELECT * FROM fields WHERE id = {chosedNumber};";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                try
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            FieldIdProperty = reader.GetInt32(0);
                            NameProperty = reader.GetString(1);
                            EctsTotalProperty = reader.GetInt16(2);
                            EctsObtainedProperty = reader.GetInt16(3);
                            YearStartingProperty = reader.GetInt16(4);
                            YearEndingProperty = reader.GetInt16(5);
                            TitleProperty = reader.GetString(6);
                        }
                        reader.Close();
                    }
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
}