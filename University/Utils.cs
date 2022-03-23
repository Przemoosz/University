namespace University;

public static class Utils
{
    public static string GetDefaultConnectionString() =>
        $"Host=localhost;Username={DefaultUsername()};Password={DefaultPassword()};Database=university;";

    private static string DefaultUsername() => "test";
    private static string DefaultPassword() => "testpassword";
}