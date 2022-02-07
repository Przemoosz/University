namespace University;

public class Subject : IData
{
    private string _name;
    private short _ects;
    private float _average;
    private float _endingGrade = 0.0f;
    private float _examGrade = 0.0f;
    private float _laboratoryGrade = 0.0f;
    private float _excerciseGrade = 0.0f;

    public string nameProperty
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

    public short ectsProperty
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

    public float averageProperty
    {
        //TODO
        get => _average;
        
    }

    public float endingGradeProperty
    {
        get => _endingGrade;
        set => _endingGrade = GradeSetLogic(value);
    }

    public float laboratoryGradeProperty
    {
        get => _laboratoryGrade;
        set => _laboratoryGrade = GradeSetLogic(value);

    }
    public float exceriseGradeProperty
    {
        get => _excerciseGrade;
        set => _excerciseGrade = GradeSetLogic(value);

    }
    public float examGradeProperty
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

        if (notZero(_excerciseGrade))
        {
            sum += _excerciseGrade;
            n++;
        }
        Console.WriteLine(sum);
        if (n != 0)
        {
            _average =(float) Math.Round((sum / n),3);
        }
        else _average = 0.0f;
    }
}
