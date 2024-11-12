using backend.Enums;

namespace backend.Models;

public class TodoDto
{
    public string? Title { get; set; }
    public State? State { get; set; }
    public string? Content { get; set; }
}