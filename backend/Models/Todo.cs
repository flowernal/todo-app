using backend.Enums;

namespace backend.Models;

public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public State State { get; set; }
    public string Content { get; set; }
}