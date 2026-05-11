using System.Data;
using AutomationDataReading.Models.Sql;
using AutomationDataReading.Services.Interfaces;
using Npgsql;

namespace AutomationDataReading.Services;

public class SqlService: ISqlReader<ProfileRecord>, ISqlReader<RoleRecord>, ISqlReader<ProfileRolesRecord>, IAsyncDisposable
{
    private const string ConnectionString = 
        "Server=localhost;Port=5432;Database=auto_records_db;User Id=postgres;Password=admin;";

    private readonly NpgsqlConnection _connection = new (ConnectionString);
    
    async Task<List<ProfileRecord>> ISqlReader<ProfileRecord>.GetList()
    {
        const string sqlQuery = "SELECT * FROM profiles";
        var reader = await MakeQuery(sqlQuery);
        var result = await ReadProfiles(reader);
        await reader.DisposeAsync();
        return result;
    }

    async Task<List<RoleRecord>> ISqlReader<RoleRecord>.GetList()
    {
        const string sqlQuery = "SELECT * FROM roles";
        var reader = await MakeQuery(sqlQuery);
        var result = await ReadRoles(reader);
        await reader.DisposeAsync();
        return result;
    }

    async Task<List<ProfileRolesRecord>> ISqlReader<ProfileRolesRecord>.GetList()
    {
        const string sqlQuery = "SELECT * FROM profile_roles";
        var reader = await MakeQuery(sqlQuery);
        var result = await ReadProfileRoles(reader);
        await reader.DisposeAsync();
        return result;
    }

    private async Task<NpgsqlDataReader> MakeQuery(string query)
    {
        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync();

        await using var command = new NpgsqlCommand(query, _connection);
        return await command.ExecuteReaderAsync();
    }

    private async Task<List<ProfileRecord>> ReadProfiles(NpgsqlDataReader reader)
    {
        var result = new List<ProfileRecord>();
        
        while (await reader.ReadAsync())
        {
            var record = new ProfileRecord
            {
                Id = reader.GetInt32("profile_id"),
                Name = reader.GetString("name"),
                Age = reader.GetInt32("age"),
                IsActive = reader.GetBoolean("is_active"),
            };
            result.Add(record);
        }
        
        return result;
    } 
    
    private async Task<List<RoleRecord>> ReadRoles(NpgsqlDataReader reader)
    {
        var result = new List<RoleRecord>();
        
        while (await reader.ReadAsync())
        {
            var record = new RoleRecord()
            {
                RoleId = reader.GetInt32("role_id"),
                Description = reader.GetString("description"),
            };
            result.Add(record);
        }
        
        return result;
    } 
    
    private async Task<List<ProfileRolesRecord>> ReadProfileRoles(NpgsqlDataReader reader)
    {
        var result = new List<ProfileRolesRecord>();
        
        while (await reader.ReadAsync())
        {
            var record = new ProfileRolesRecord()
            {
                ProfileId = reader.GetInt32("profile_id"),
                RoleId = reader.GetInt32("role_id"),
            };
            result.Add(record);
        }
        
        return result;
    } 
    
    public async ValueTask DisposeAsync()
    {
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}