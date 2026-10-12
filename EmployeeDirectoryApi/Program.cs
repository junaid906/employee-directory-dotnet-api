using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Data.Seed;
using EmployeeDirectoryApi.Endpoints.Departments;
using EmployeeDirectoryApi.Endpoints.Employees;
using EmployeeDirectoryApi.Endpoints.Positions;
using EmployeeDirectoryApi.Endpoints.SubDepartments;
using EmployeeDirectoryApi.Handlers;
using EmployeeDirectoryApi.Options;
using EmployeeDirectoryApi.Services.Department;
using EmployeeDirectoryApi.Services.Employee;
using EmployeeDirectoryApi.Services.Position;
using EmployeeDirectoryApi.Services.SubDepartment;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var authSettings = builder.Configuration.GetSection(AuthSettings.SectionName).Get<AuthSettings>()
    ?? throw new InvalidOperationException($"Missing '{AuthSettings.SectionName}' configuration section.");

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        // change headrer later dumb dumb
        policy.WithOrigins("http://localhost:5174")
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


builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        options.SetIssuer(authSettings.Issuer);
        options.AddAudiences(authSettings.Audience);
        options.UseSystemNetHttp();
        options.UseAspNetCore();
    });

builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
builder.Services.AddAuthorization(options =>
    options.AddPolicy("Tool", policy => policy
        .AddAuthenticationSchemes(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .RequireAssertion(ctx => ctx.User.HasScope(authSettings.Scope))));

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors("FrontendPolicy");
app.UseAuthentication();
app.UseAuthorization();

// remove this shit later
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDirectoryDbContext>();
    await dbContext.Database.MigrateAsync();
    await DbSeeder.SeedAsync(dbContext);
}

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