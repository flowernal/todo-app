using System.Data;
using Dapper;
using Npgsql;

namespace backend.Services;

public class DbService(IConfiguration configuration) : IDbService
{
    private readonly IDbConnection _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

    public async Task<T?> GetAsync<T>(string command, object parameters) =>
        (await _db.QueryAsync<T>(command, parameters).ConfigureAwait(false)).FirstOrDefault();

    public async Task<List<T>> GetAll<T>(string command, object parameters) =>
        (await _db.QueryAsync<T>(command, parameters)).ToList();

    public async Task<int> EditData(string command, object parameters) =>
        await _db.ExecuteAsync(command, parameters);
}