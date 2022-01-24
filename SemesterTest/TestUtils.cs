using System;
using Npgsql;
using University;

namespace SemesterTest;

public class TestUtils
{
    public static void TableDrop()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string command = "DROP TABLE IF EXISTS semester CASCADE;";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, connection))
                cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public static bool TableExists()
    {
        bool exists;
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string command = "SELECT EXISTS(SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'semester')";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, connection))
                exists = Convert.ToBoolean(cmd.ExecuteScalar().ToString());
            connection.Close();
        }

        return exists;
    }

    public static void CreateSimpleTableToDrop()
    {
        // This table should never be in use!
        // Only for testing purpose
        using (NpgsqlConnection connection = new NpgsqlConnection(Utils.getDefaultConnectionString()))
        {
            connection.Open();
            string command = "CREATE TABLE IF NOT EXISTS semester (id SERIAL PRIMARY KEY)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(command, connection))
                cmd.ExecuteNonQuery();
            connection.Close();
        }
        
        
    }
    
}