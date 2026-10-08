using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Data.Seed;
using EmployeeDirectoryApi.Endpoints.Departments;
using EmployeeDirectoryApi.Endpoints.Employees;
using EmployeeDirectoryApi.Endpoints.Positions;
using EmployeeDirectoryApi.Endpoints.SubDepartments;
using EmployeeDirectoryApi.Handlers;
using EmployeeDirectoryApi.Services.Department;
using EmployeeDirectoryApi.Services.Employee;
using EmployeeDirectoryApi.Services.Position;
using EmployeeDirectoryApi.Services.SubDepartment;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        // change headrer later dumb dumb
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<EmployeeDirectoryDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Employees")));
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<PositionService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SubDepartmentService>();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

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