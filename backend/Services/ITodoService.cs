using backend.Enums;
using backend.Models;

namespace backend.Services;

public interface IItemService
{
    Task<bool> CreateItem(Todo todo);
    Task<List<Todo>> GetItemList();
    Task<List<Todo>> GetItemList(State[] states);
    Task<Todo?> UpdateItem(Todo todo);
    Task<bool> DeleteItem(Guid id);
}