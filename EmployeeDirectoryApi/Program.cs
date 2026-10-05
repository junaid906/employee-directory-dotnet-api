using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Data.Seed;
using EmployeeDirectoryApi.Endpoints.Employees;
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

builder.Services.AddDbContext<EmployeeDirectoryDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Employees")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
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

app.MapGet("/", () => "Hello World!");
app.MapGetEmployees();
app.Run();