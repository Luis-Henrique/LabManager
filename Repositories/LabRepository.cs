using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class LabRepository
{

    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig) 
    {
        _databaseConfig = databaseConfig;
    }

    public IEnumerable<Lab> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var labs = connection.Query<Lab>("SELECT * FROM Labs");
        
        connection.Close();

        return labs;
    }

    public Lab Save(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Labs VALUES(@Id, @Number, @Name, @Block)", lab);
        
        connection.Close();
        return lab;
    }

    public Lab Update(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Labs SET number = ($number), name = ($name), block = ($block) WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", lab.Id);
        command.Parameters.AddWithValue("$number", lab.Number);
        command.Parameters.AddWithValue("$name", lab.Name);
        command.Parameters.AddWithValue("$block", lab.Block);

        command.ExecuteNonQuery();
        connection.Close();

        return lab;
    }

    public Lab GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Labs WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", id);
        var reader = command.ExecuteReader();

        reader.Read(); //uma linha só pra ler (por isso não esta no while)
        var lab = ReaderToLab(reader); 

        connection.Close();
        return lab;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Labs WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", id);
        
        command.ExecuteNonQuery();
        connection.Close();
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Labs WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", id);
        
        /*
        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);
        */

        var result = Convert.ToBoolean(command.ExecuteScalar());

        return result;
    }

    private Lab ReaderToLab(SqliteDataReader reader)
    {
        var lab = new Lab(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

        return lab;
    }
}