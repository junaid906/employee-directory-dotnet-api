using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Endpoints;
using EmployeeDirectoryApi.Services.Employee;
using EmployeeDirectoryApi.Services.Employee.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Employees")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

app.UseCors("FrontendPolicy");

app.MapGet("/", () => "Hello World!");
app.MapEmployees();
app.Run();