using Biblioteca.Api.EndPoints;
using Biblioteca.Api.Middleware;
using Biblioteca.Infrastructure.IoC.DependecyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .RegisterDataBase(builder.Configuration)
    .RegisterRepositories()
    .RegisterServices()
    .RegisterProviders()
    .RegisterLibraries()
    .RegisterRepositoriesModule();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        b => b
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true));
} );


var app = builder.Build();

app.UseCors("CORSPolicy");  
app.UseMiddleware<MiddlewareException>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//add your endpoints
app.MapUsuarioEndpoints();
app.MapLibroEndpoints();

app.Run();