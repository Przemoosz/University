namespace University;

public static class Utils
{
    public static string GetDefaultConnectionString() =>
        $"Host=localhost;Username={Defaultusername()};Password={DefaultPassword()};Database=university;";

    private static string Defaultusername() => "test";
    private static string DefaultPassword() => "testpassword";
}