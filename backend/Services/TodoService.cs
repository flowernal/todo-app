using backend.Enums;
using backend.Models;

namespace backend.Services;

public class ItemService(IDbService dbService) : IItemService
{
    public async Task<bool> CreateItem(Todo todo)
    {
        var rowsAffected =
            await dbService.EditData(
                "INSERT INTO public.item (id, title, state, content) VALUES (@Id, @Title, @State, @Content)",
                todo);

        return rowsAffected > 0;
    }

    public async Task<List<Todo>> GetItemList() =>
        await dbService.GetAll<Todo>("SELECT * FROM public.item", new { });
    
    public async Task<List<Todo>> GetItemList(State[] states) =>
        await dbService.GetAll<Todo>("SELECT * FROM public.item WHERE state = ANY(@states)", new { states });

    public async Task<Todo?> GetItem(Guid id) =>
        await dbService.GetAsync<Todo>("SELECT * FROM public.item where id=@id", new { id });

    public async Task<Todo?> UpdateItem(Todo todo)
    {
        var rowsAffected =
            await dbService.EditData(
                "Update public.item SET title=@Title state=@State content=@Content WHERE id=@Id",
                todo);
        
        return rowsAffected > 0 ? todo : null;
    }

    public async Task<bool> DeleteItem(Guid id)
    {
        var rowsAffected = await dbService.EditData("DELETE FROM public.item WHERE id=@Id", new { id });
        return rowsAffected > 0;
    }
}