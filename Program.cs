using Microsoft.EntityFrameworkCore;
using GestorCursosApi.Data;
using DotNetEnv;

Env.Load();

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

var server = Environment.GetEnvironmentVariable("MYSQL_SERVER") ;
var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ;
var database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ;
var user = Environment.GetEnvironmentVariable("MYSQL_USER") ;
var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ;

var connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};SslMode=none;AllowPublicKeyRetrieval=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
    );

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
