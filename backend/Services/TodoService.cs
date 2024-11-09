using backend.Enums;
using backend.Models;

namespace backend.Services;

public class TodoService(IDbService dbService) : ITodoService
{
    public async Task<bool> CreateItem(Todo todo)
    {
        var rowsAffected =
            await dbService.EditData(
                "INSERT INTO todos (title, state, content) VALUES (@Title, @State, @Content)",
                todo);

        return rowsAffected > 0;
    }

    public async Task<List<Todo>> GetItemList() =>
        await dbService.GetAll<Todo>("SELECT * FROM todos", new { });
    
    public async Task<List<Todo>> GetItemList(State[] states) =>
        await dbService.GetAll<Todo>(
            "SELECT * FROM todos WHERE state = ANY(@states)",
            new { states = Array.ConvertAll(states, state => (int) state) });

    public async Task<Todo?> GetItem(Guid id) =>
        await dbService.GetAsync<Todo>("SELECT * FROM todos WHERE id=@id", new { id });

    public async Task<Todo?> UpdateItem(Guid id, Todo todo)
    {
        todo.Id = id;
        
        var rowsAffected =
            await dbService.EditData(
                "UPDATE todos SET title=@title, state=@state, content=@content WHERE id=@id",
                new { id = todo.Id, title = todo.Title, state = (int) todo.State, content = todo.Content });
        
        return rowsAffected > 0 ? todo : null;
    }

    public async Task<bool> DeleteItem(Guid id)
    {
        var rowsAffected = await dbService.EditData("DELETE FROM todos WHERE id=@Id", new { id });
        return rowsAffected > 0;
    }
}