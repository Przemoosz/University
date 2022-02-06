namespace University;

public class Subject: IData
{
    private string _name;
    private short _ects;
    private float _average;
    
    public string nameProperty { get; set; }
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