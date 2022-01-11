namespace University;

public static class Utils
{
    public static string getDefaultConnectionString() =>
        $"Host=localhost;Username={defaultusername()};Password={defaultPassword()};Database=university;";

    private static string defaultusername() => "test";
    private static string defaultPassword() => "testpassword";
}