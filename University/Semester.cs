using Npgsql;
using NpgsqlTypes;
namespace University;

public sealed class Semester: IData
{
    private string _name  = "None";
    private short _ectsTotal = 0;
    private short _ectsObtained = 0;
    private float _average = 0.0f;
    private string _title;
    private int _fieldIdReference;
    private int _semesterId;

    public Semester()
    {
        
    }
    public Semester(string title)
    {
        TitleProperty = title;
    }

    public Semester(string title, int fieldInt)
    {
        _title = title;
        _fieldIdReference = fieldInt;
    }

    public string TitleProperty
    {
        get => _title;
        set
        {
            if (value.Equals("Engineer Degree"))
            {
                _title = "ED";
            }
            else if (value.Equals("Master Engineer Degree"))
            {
                _title = "MED";
            }
            else
            {
                // TODO
                throw new ArgumentException("Something went Wrong!");
            }
        }
    }
    public string NameProperty
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

    public short EctsTotalProperty
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
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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

            NameProperty = number switch
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

            NameProperty = number switch
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
        EctsTotalProperty = providedEcts;
    }

    public void CreateSemester()
    {
        if (!this.TableExists())
        {
            CreateTable();
        }
        
        ProvideEctsTotal();
        DataInsertion();
    }

    public void DropTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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
    public static bool TableExistsStatic()
    {
        bool exists;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd =
                "INSERT INTO semester VALUES(DEFAULT, @Name, @EctsTotal, DEFAULT, DEFAULT, @IdReference) RETURNING id;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                command.Parameters.Add("@Name", NpgsqlDbType.Varchar);
                command.Parameters.Add("@EctsTotal", NpgsqlDbType.Smallint);
                command.Parameters.Add("@IdReference", NpgsqlDbType.Integer);
                command.Parameters["@Name"].Value = NameProperty;
                command.Parameters["@EctsTotal"].Value = EctsTotalProperty;
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
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
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

    public void ConnectToField(IDataInput dataInput)
    {
        // Using Dependency Injection to take input from console
        int rows;
        try
        {
            rows = Field.AllFieldsInDatabase();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fields probably does not exists! Contact Developer to fix problem, or re run setup");
            Console.WriteLine(e);
            throw;
        }
        
        Console.WriteLine("Type Id of Filed to connect: ");
        string receivedData = dataInput.GetInput();
        int id;
        if (Int32.TryParse(receivedData, out id))
        {
            if (id > rows || id <= 0)
            {
                throw new Exception("Id Can not be lowe than zero or higher than max Id");
            }
            _fieldIdReference = id;
        }
        else
        {
            throw new Exception("Can not Convert to Integer data type!");
        }
        Console.WriteLine(rows);
    }

    public static int AllSemesterInDatabase()
    {
        int rows = 0;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT * FROM semester;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd,connection))
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("Id -- Name -- EctsTotal -- EctsObtained -- Average -- FieldReferenceID");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetInt32(0)} -- {reader.GetString(1)} -- {reader.GetInt16(2)} -- {reader.GetInt16(3)} -- {reader.GetDouble(4)} -- {reader.GetInt32(5)}");
                        rows++;
                    }  
                }
                reader.Close();
            }
            connection.Close();
        }
        return rows;
    }

    public void LoadDataFromDatabase(IDataInput dataInput)
    {
        int maxId = 0;
        int choosed;
        // Not tested
        using (NpgsqlConnection connection= new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd =
                "SELECT semester.id AS sem_id, semester.name AS sem_name, fields.name AS field_name FROM SEMESTER INNER JOIN fields ON field_id = fields.id;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("sem_id -- sem_name -- field_name");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetInt32(0)} -- {reader.GetString(1)} -- {reader.GetString(2)}");
                            maxId = reader.GetInt32(0);
                        }
                    
                    }
                    else
                    {
                        connection.Close();
                        Console.WriteLine("There is no rows in table! Create some semester and save it to database");
                        throw new Exception("NO rows available");
                    }
                }
            }
            Console.WriteLine("Choose Id number to load semseter: ");
            string choosedString = dataInput.GetInput();
            if (Int32.TryParse(choosedString, out choosed))
            {
                if (choosed > maxId || choosed <= 0)
                {
                    throw new Exception("Choosed wrong Id!");
                }
            }
            else
            {
                throw new Exception("Cant parse input to Int32!");
            }
            // TODO
            // Change select to select with inner join and get title name!
            cmd = $"SELECT * FROM semester WHERE id = {choosed};";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                try
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            // To remove!
                            _title = "ED";
                            _semesterId = reader.GetInt32(0);
                            NameProperty = reader.GetString(1);
                            EctsTotalProperty = reader.GetInt16(2);
                            // TODO
                            // Property to implement!
                            _ectsObtained = reader.GetInt16(3);
                            // Property to implement!
                            _average = (float) reader.GetDouble(4);
                            _fieldIdReference = reader.GetInt32(5);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            connection.Close();
        }

        
    }

    public void ShowConnectedSubjects()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd =
                $"SELECT * FROM semester INNER JOIN subjects ON semester.id=subjects.semester_reference_id;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd,connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("ID -- Semester Name -- Subject Name -- Subject ECTS -- Subject.Average -- Subject Ending Grade");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetInt32(0)} -- {reader.GetString(1)} -- {reader.GetString(7)} -- {reader.GetInt16(8)} -- {reader.GetDouble(9)} -- {reader.GetDouble(10)}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows available for subjects!");
                    }
                }
                
            }
        }
    }
}