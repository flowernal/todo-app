using backend.Classes;
using backend.Enums;
using backend.Models;
using backend.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UsePathBase("/api");

DefaultTypeMap.MatchNamesWithUnderscores = true;

app.MapGet("/todo", async (
    [FromQuery(Name = "state")] string? state,
    [FromServices] ITodoService db) =>
    {
        var states = new List<State>();

        switch (state)
        {
            case "created":
                states.Add(State.Open);
                states.Add(State.InProgress);
                break;
            case "finished":
                states.Add(State.Finished);
                break;
            case "all":
            case null:
                states.Add(State.Open);
                states.Add(State.InProgress);
                states.Add(State.Finished);
                break;
            default:
                return Results.BadRequest(new ErrorResponse
                {
                    IsError = true,
                    Error = new Error
                    {
                        Code = ErrorCode.InvalidStateQuery,
                        Message = "Invalid state query"
                    }
                });
        }

        return Results.Ok(await db.GetItemList(states.ToArray()));
    })
    .WithName("GetTodos");

app.MapGet("/todo/{id:guid}", async (
    [FromRoute] Guid id,
    [FromServices] ITodoService db) =>
    {
        var todo = await db.GetItem(id);

        if (todo is null)
        {
            return Results.NotFound(new ErrorResponse
            {
                IsError = true,
                Error = new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "This todo was not found"
                }
            });
        }

        return Results.Ok(todo);
    })
    .WithName("GetTodo");

app.MapPost("/todo", async (
    [FromBody] Todo todo,
    [FromServices] ITodoService db) =>
    {
        var rowsAffected = await db.CreateItem(todo);

        if (rowsAffected == 0)
        {
            return Results.BadRequest(new ErrorResponse
            {
                IsError = true,
                Error = new Error
                {
                    Code = ErrorCode.InvalidTodo,
                    Message = "Invalid todo"
                }
            });
        }

        return Results.Ok();
    })
    .WithName("CreateTodo");

app.MapPut("/todo/{id:guid}", async (
    [FromRoute] Guid id,
    [FromBody] TodoDto dto,
    [FromServices] ITodoService db) =>
    {
        var rowsAffected = await db.UpdateItem(id, dto);

        if (rowsAffected == 0)
        {
            return Results.BadRequest(new ErrorResponse
            {
                IsError = true,
                Error = new Error
                {
                    Code = ErrorCode.InvalidTodo,
                    Message = "Invalid todo"
                }
            });
        }

        return Results.Ok();
    })
    .WithName("PutTodo");

app.MapDelete("/todo/{id:guid}", async (
    [FromRoute] Guid id,
    [FromServices] ITodoService db) =>
    {
        var rowsAffected = await db.DeleteItem(id);

        if (rowsAffected == 0)
        {
            return Results.NotFound(new ErrorResponse
            {
                IsError = true,
                Error = new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "This todo was not found"
                }
            });
        }

        return Results.Ok();
    })
    .WithName("DeleteTodo");

app.Run();
