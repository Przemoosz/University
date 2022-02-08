using Npgsql;
using NpgsqlTypes;

namespace University;

public class Subject : IData
{
    private string _name = "3dsadasd";
    private short _ects = 32;
    private float _average =0.0f;
    private float _endingGrade = 0.0f;
    private float _examGrade = 0.0f;
    private float _laboratoryGrade = 0.0f;
    private float _exerciseGrade = 0.0f;
    private int _subjectId;
    private int _semester_id = 1;

    public string NameProperty
    {
        get { return _name; }
        set
        {
            if (value.Length == 0 || value.Length > 100)
            {
                throw new ArgumentException("Provided too long or blank name!");
            }

            _name = value;
        }
    }

    public void ProvideName()
    {
        throw new NotImplementedException();
    }

    public void CreateTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = @"CREATE TABLE IF NOT EXISTS subjects (
            id SERIAL PRIMARY KEY,
            name VARCHAR(100) NOT NULL,
            ects SMALLINT DEFAULT 0,
            average FLOAT DEFAULT 0,
            ending_grade FLOAT DEFAULT 0,
            exam_grade FLOAT DEFAULT 0,
            labolatory_grade FLOAT DEFAULT 0,
            exercise_grade FLOAT DEFAULT 0,
            semester_reference_id INT REFERENCES semester(id));";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                 connection.Close();   
                }
            }
        }
    }

    public void DropTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "DROP TABLE IF EXISTS subjects CASCADE;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public bool TableExists()
    {
        bool exists;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string cmd = "SELECT EXISTS(SELECT FROM pg_tables WHERE schemaname='public' AND tablename='subjects')";
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
            string cmd = "INSERT INTO subjects VALUES (DEFAULT, @NAME, @ECTS, @AVERAGE, @END, @EXAM, @LAB, @EXE, @REFERENCEID) RETURNING id;";
            using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
            {
                command.Parameters.Add("@NAME", NpgsqlDbType.Varchar);
                command.Parameters.Add("@ECTS", NpgsqlDbType.Smallint);
                command.Parameters.Add("@AVERAGE", NpgsqlDbType.Double);
                command.Parameters.Add("@END", NpgsqlDbType.Double);
                command.Parameters.Add("@EXAM", NpgsqlDbType.Double);
                command.Parameters.Add("@LAB", NpgsqlDbType.Double);
                command.Parameters.Add("@EXE", NpgsqlDbType.Double);
                command.Parameters.Add("@REFERENCEID", NpgsqlDbType.Integer);
                command.Parameters["@NAME"].Value = NameProperty;
                command.Parameters["@ECTS"].Value =EctsProperty;
                command.Parameters["@AVERAGE"].Value = (double) AverageProperty;
                command.Parameters["@END"].Value = (double) EndingGradeProperty;
                command.Parameters["@EXAM"].Value = (double) ExamGradeProperty;
                command.Parameters["@LAB"].Value = (double) LaboratoryGradeProperty;
                command.Parameters["@EXE"].Value = (double) ExceriseGradeProperty;
                command.Parameters["@REFERENCEID"].Value = _semester_id;
                try
                {
                    var reader = command.ExecuteScalar();
                    _subjectId = Convert.ToInt32(reader);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }

    public short EctsProperty
    {
        get => _ects;
        set
        {
            if (value < 0 || value > 10)
            {
                throw new ArgumentException("ECTS can not be negative number or greater than 10!");
            }

            _ects = value;
        }
    }

    public float AverageProperty
    {
        get => _average;
        set => _average = value;
    }

    public float EndingGradeProperty
    {
        get => _endingGrade;
        set => _endingGrade = GradeSetLogic(value);
    }

    public float LaboratoryGradeProperty
    {
        get => _laboratoryGrade;
        set => _laboratoryGrade = GradeSetLogic(value);

    }
    public float ExceriseGradeProperty
    {
        get => _exerciseGrade;
        set => _exerciseGrade = GradeSetLogic(value);

    }
    public float ExamGradeProperty
    {
        get => _examGrade;
        set => _examGrade = GradeSetLogic(value);

    }

    private float GradeSetLogic(float input)
    {

        // According to polish grade system in universities possible grade are: 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0 
        // 0.0 is for not know grade
        if (input == 2.0f || input == 2.5f || input == 3.0f || input == 3.5f || input == 4.0f || input == 4.5f ||
            input == 5.0f || input == 0.0f)
        {
            return input;
        }
        else
        {
            throw new ArgumentException(
                "According to polish grade system in universities possible grades are: 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0! 0.0 is for not know grade!");
        }
    }

    public void AverageCalculate()
    {
        float sum = 0.0f;
        int n = 0;
        Predicate<float> notZero = f => f != 0.0f;
        // Console.WriteLine("Here");
        if (notZero(_examGrade))
        {
            // Console.WriteLine("Here");
            sum += _examGrade;
            n++;
        }

        if (notZero(_laboratoryGrade))
        {
            sum += _laboratoryGrade;
            n++;
        }

        if (notZero(_exerciseGrade))
        {
            sum += _exerciseGrade;
            n++;
        }
        Console.WriteLine(sum);
        if (n != 0)
        {
            AverageProperty =(float) Math.Round((sum / n),3);
        }
        else AverageProperty = 0.0f;
    }
}
