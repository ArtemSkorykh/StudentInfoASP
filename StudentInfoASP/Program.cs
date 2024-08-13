using Microsoft.Extensions.Options;
using StudentInfoASP.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("student.json");

builder.Services.Configure<Student>(builder.Configuration.GetSection("Student"));

var app = builder.Build();

var color = app.Configuration["color"] ?? "black";

app.MapGet("/home", (IOptions<Student> studentOptions) =>
{
    var student = studentOptions.Value;
    return Results.Content($"<html><body style='color:{color};'>" +
                           $"Name: {student.FirstName} {student.LastName}<br/>" +
                           $"Age: {student.Age}" +
                           $"</body></html>", "text/html");
});

app.MapGet("/academy", (IOptions<Student> studentOptions) =>
{
    var disciplines = string.Join(", ", studentOptions.Value.Disciplines);
    return Results.Content($"<html><body style='color:{color};'>" +
                           $"Disciplines: {disciplines}" +
                           $"</body></html>", "text/html");
});

app.Run();