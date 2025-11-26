using Microsoft.EntityFrameworkCore;
using GestorCursosApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
} 
);

var server = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? "localhost";
var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
var database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "db_gestorcursos";
var user = Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root";
var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "mysql";

var connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};SslMode=none;AllowPublicKeyRetrieval=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 1, 0))));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gestor de Tareas API",
        Version = "v1",
        Description = "API REST para gestion de tareas",
    })

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestor de Cursos API V1");
        c.RoutePrefix = "swagger";
    }

    );
}
app.UseCors("PermitirTodo");
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
