using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StudentInfoASP.Models;

namespace StudentInfoASP
{
    public class StudentMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<Student> _studentOptions;

        public StudentMiddleware(RequestDelegate next, IOptions<Student> studentOptions)
        {
            _next = next;
            _studentOptions = studentOptions;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (path == "/home")
            {
                var student = _studentOptions.Value;
                var color = context.Request.Query["color"].ToString() ?? "black";

                await context.Response.WriteAsync($"<html><body style='color:{color};'>" +
                                                  $"Name: {student.FirstName} {student.LastName}<br/>" +
                                                  $"Age: {student.Age}" +
                                                  $"</body></html>");
                return;
            }
            else if (path == "/academy")
            {
                var student = _studentOptions.Value;
                var color = context.Request.Query["color"].ToString() ?? "black";
                var disciplines = string.Join(", ", student.Disciplines);

                await context.Response.WriteAsync($"<html><body style='color:{color};'>" +
                                                  $"Disciplines: {disciplines}" +
                                                  $"</body></html>");
                return;
            }

            await _next(context);
        }
    }
}
