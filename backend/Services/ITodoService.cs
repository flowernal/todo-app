using backend.Enums;
using backend.Models;

namespace backend.Services;

public interface ITodoService
{
    Task<int> CreateItem(Todo todo);
    Task<List<Todo>> GetItemList();
    Task<List<Todo>> GetItemList(State[] states);
    Task<Todo?> GetItem(Guid id);
    Task<int> UpdateItem(Guid id, TodoDto dto);
    Task<int> DeleteItem(Guid id);
}