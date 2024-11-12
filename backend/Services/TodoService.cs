using backend.Enums;
using backend.Models;
using Dapper;

namespace backend.Services;

public class TodoService(IDbService dbService) : ITodoService
{
    public async Task<int> CreateItem(Todo todo) =>
        await dbService.EditData(
            "INSERT INTO todos (title, state, content) VALUES (@Title, @State, @Content)",
            todo);

    public async Task<List<Todo>> GetItemList() =>
        await dbService.GetAll<Todo>("SELECT * FROM todos", new { });

    public async Task<List<Todo>> GetItemList(State[] states) =>
        await dbService.GetAll<Todo>(
            "SELECT * FROM todos WHERE state = ANY(@states)",
            new { states = Array.ConvertAll(states, state => (int)state) });

    public async Task<Todo?> GetItem(Guid id) =>
        await dbService.GetAsync<Todo>("SELECT * FROM todos WHERE id=@id", new { id });

    public async Task<int> UpdateItem(Guid id, TodoDto dto)
    {
        var sql = "UPDATE todos SET ";
        var parameters = new DynamicParameters();

        if (dto.Title is not null)
        {
            sql += "title = @Title, ";
            parameters.Add("Title", dto.Title);
        }
        if (dto.State is not null)
        {
            sql += "state = @State, ";
            parameters.Add("State", dto.State);
        }
        if (dto.Content is not null)
        {
            sql += "content = @Content, ";
            parameters.Add("Content", dto.Content);
        }

        sql = sql.TrimEnd(',', ' ');

        sql += " WHERE id = @Id";
        parameters.Add("Id", id);

        var rowsAffected = await dbService.EditData(sql, parameters);
        return rowsAffected;
    }

    public async Task<int> DeleteItem(Guid id) =>
        await dbService.EditData("DELETE FROM todos WHERE id=@Id", new { id });
}