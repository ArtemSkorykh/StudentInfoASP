using Microsoft.Extensions.Options;
using StudentInfoASP.Models;
using StudentInfoASP;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("student.json", optional: true, reloadOnChange: true);

builder.Services.Configure<Student>(builder.Configuration.GetSection("Student"));

var app = builder.Build();

app.UseMiddleware<StudentMiddleware>();

app.Run();