using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

var listEmpleoyee = Enumerable.Range(1, 20).Select(
    id => new Empleoyee()
    {
        Id = id,
        Name = $"Name {id}",
    }).ToList();

app.MapGet("/empleoyee/get", () =>
{
    return listEmpleoyee;
});

app.MapGet("/empleoyee/get/{id}", (int id) =>
{
    return listEmpleoyee.Where(x => x.Id == id).LastOrDefault();
});

app.MapPost("/empleoyee/post", ([FromBody] Empleoyee empleoyee) =>
{
    listEmpleoyee.Add(empleoyee);
});

app.MapPut("/empleoyee/put/{id}", (int id, [FromBody] Empleoyee empleoyee) =>
{
    Empleoyee? empleoyeeTemp = listEmpleoyee.Where(x => x.Id == id).LastOrDefault();

    if (empleoyeeTemp is not null)
    {
        empleoyeeTemp.Name = empleoyee.Name;
    }
});

app.MapDelete("/empleoyee/delete/{id}", (int id) =>
{
    Empleoyee? empleoyeeTemp = listEmpleoyee.Where(x => x.Id == id).LastOrDefault();

    if (empleoyeeTemp is not null)
    {
        listEmpleoyee.Remove(empleoyeeTemp);
    }
});

app.Run();