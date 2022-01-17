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

    public Semester(string title)
    {
        _title = title;
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
    
    public void CreateField()
    {
        throw new NotImplementedException();
    }

    public void ProvideName()
    {
        throw new NotImplementedException();
    }

    public void CreateTable()
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