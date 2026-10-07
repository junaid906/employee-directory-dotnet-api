using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Data.Seed;
using EmployeeDirectoryApi.Endpoints.Departments;
using EmployeeDirectoryApi.Endpoints.Employees;
using EmployeeDirectoryApi.Endpoints.Positions;
using EmployeeDirectoryApi.Endpoints.SubDepartments;
using EmployeeDirectoryApi.Services.Department;
using EmployeeDirectoryApi.Services.Department.Interfaces;
using EmployeeDirectoryApi.Services.Employee;
using EmployeeDirectoryApi.Services.Employee.Interfaces;
using EmployeeDirectoryApi.Services.Position;
using EmployeeDirectoryApi.Services.Position.Interfaces;
using EmployeeDirectoryApi.Services.SubDepartment;
using EmployeeDirectoryApi.Services.SubDepartment.Interfaces;
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

builder.Services.AddDbContext<EmployeeDirectoryDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Employees")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ISubDepartmentService, SubDepartmentService>();
builder.Services.AddOpenApi();

var app = builder.Build();


// remove this shit later
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDirectoryDbContext>();
    await dbContext.Database.MigrateAsync();
    await DbSeeder.SeedAsync(dbContext);
}

app.UseCors("FrontendPolicy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Employee API V1");
    });
}

app.MapEmployees();
app.MapPositions();
app.MapDepartments();
app.MapSubDepartments();
app.Run();