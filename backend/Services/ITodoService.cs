using backend.Enums;
using backend.Models;

namespace backend.Services;

public interface ITodoService
{
    Task<bool> CreateItem(Todo todo);
    Task<List<Todo>> GetItemList();
    Task<List<Todo>> GetItemList(State[] states);
    Task<Todo?> GetItem(Guid id);
    Task<Todo?> UpdateItem(Guid id, Todo todo);
    Task<bool> DeleteItem(Guid id);
}