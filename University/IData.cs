namespace University;

public interface IData
{
    string NameProperty { get; set; }
    // void CreateField();
    void ProvideName();
    void CreateTable();
    void DropTable();
    bool TableExists();
}
