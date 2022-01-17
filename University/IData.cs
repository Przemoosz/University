namespace University;

public interface IData
{
    string nameProperty { get; set; }
    // void CreateField();
    void ProvideName();
    void CreateTable();
    void DropTable();
    bool TableExists();
}
