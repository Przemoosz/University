namespace University;

public class Field
{
    private string name = "None";
    private short ectsTotal = 0;
    private short ectsObtained = 0;
    private short yearStarting = 1970;
    private short yearEnding = 1970;

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
        set => yearStarting = value;
    }
}