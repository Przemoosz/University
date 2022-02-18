namespace University;

public sealed class GetInputClass:IDataInput
{
    public string GetInput()
    {
        return Console.ReadLine().Trim();
    }
}