using Npgsql;
using NpgsqlTypes;
namespace University;

public class Semester: IData
{
    private string _name  = "None";
    private short _ectsTotal = 0;
    private short _ectsObtained = 0;
    private float _average = 0.0f;
    private string _title = "ED";
    private int _fieldIdReference;

    public Semester(string title)
    {
        _title = title;
    }

    public Semester(string title, int fieldInt)
    {
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

    public short ecstTotalProperty
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
        throw new NotImplementedException();
    }

    public void CreateField()
    {
        throw new NotImplementedException();
    }

    public void DropTable()
    {
        throw new NotImplementedException();
    }

    public bool TableExists()
    {
        throw new NotImplementedException();
    }
}