namespace University;

public sealed class GetInputClass:IDataInput
{
    public string GetInput()
    {
        string? ret = Console.ReadLine().Trim();
        if (ret is not null)
        {
            return ret;
        }
        else
        {
            return "";
        }
    }
}