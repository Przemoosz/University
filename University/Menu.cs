namespace University;

public static class Menu
{
    private static string getDegree()
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

        providedTitle= num switch
        {
            1 => "Engineer Degree",
            2 => "Master Engineer Degree",
            _ => "None"
        };
        return providedTitle;
    }
    public static void CreateField()
    {
        Field field = new Field();
        if (!field.TableExists())
        {
            field.CreateTable();
            Console.WriteLine("Created Table - Fields");
        }
        field.CreateField();
        Console.WriteLine($"Created Field {field.NameProperty}");
    }

    public static void CreateSemester()
    {
        if (!Field.TableExistsStatic())
        {
            Console.WriteLine("Field Table does not exists! You should create first Field object");
            return;
        }

        string providedTitle = getDegree();
        Semester semester = new Semester(providedTitle);
        if (!semester.TableExists())
        {
            semester.CreateTable();
            Console.WriteLine("Created Table - Semester");
        }
        semester.ConnectToField(new GetInputClass());
        semester.CreateSemester();
    }

    public static void CreateSubject()
    {
        if (!Field.TableExistsStatic())
        {
            Console.WriteLine("Field Table does not exists! You should create first Field object");
            return;
        }

        if (!Semester.TableExistsStatic())
        {
            Console.WriteLine("Field Table does not exists! You should create first Field object");
            return;
        }

        Subject subject = new Subject();
        if (!subject.TableExists())
        {
            subject.CreateTable();
            Console.WriteLine("Created Table - Subjects");
        }
        subject.CreateSubject();
    }

    public static void ShowConnectedSemesters()
    {
        Field field = new Field();
        if (!field.TableExists())
        {
            Console.WriteLine("Table Fields does not exists!");
            return;
        }

        if (!Semester.TableExistsStatic())
        {
            Console.WriteLine("There is no semester table! Create new and return here");
            return;
        }
        field.LoadFieldFromDatabase();
        field.ShowConnectedSemesters();
    }

    public static void ShowConnectedSubjcts()
    {
        Semester semester = new Semester();
        if (!Field.TableExistsStatic())
        {
            Console.WriteLine("Table Fields does not exists!");
            return;
        }

        if (!Semester.TableExistsStatic())
        {
            Console.WriteLine("Table Semester does not exists!");
            return;
        }
        
        semester.ShowConnectedSubjects();
    }
}