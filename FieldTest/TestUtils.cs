using System;
using System.Reflection.Metadata;
using University;
using Npgsql;
namespace FieldTest;
public static class TestUtils
{
    public static void TableDrop()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string command = "DROP TABLE IF EXISTS fields CASCADE;";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static void SimpleTableCreate()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string command = "CREATE TABLE IF NOT EXISTS fields(id SERIAL PRIMARY KEY);";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static bool TableExists()
    {
        bool result = false;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.GetDefaultConnectionString()))
        {
            connection.Open();
            string command =
                "SELECT EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'fields')";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, connection))
            {
                result = Convert.ToBoolean(cmd.ExecuteScalar().ToString());
                
            }
            connection.Close();
        }

        return result;

    }
}