using Biblioteca.Application.Services.Biblioteca;
using Biblioteca.Domain.Repositories;
using Biblioteca.Domain.Repositories.Biblioteca;
using Biblioteca.Domain.Repositories.Common;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Context;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Biblioteca;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Biblioteca.Infrastructure.IoC.DependecyInjection
{
    public static class BibliotecaDI
    {
        public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            collection.AddDbContext<BibliotecaDBcontext>(options =>
            {
                options.UseSqlServer(connectionString);
            }
            );
            return collection;
        }
        public static IServiceCollection RegisterRepositoriesModule(this IServiceCollection collection)
        {
            // Registrar el UnitOfWork y cualquier módulo de repositorios
            collection.AddTransient<IUnitOfWork, UnitOfWork>();
            collection.AddTransient<IBibliotecaRepositoriesModule, BibliotecaRepositoriesModule>();
            return collection;
        }

        public static IServiceCollection RegisterLibraries(this IServiceCollection collection)
        {
            // Registrar FluentValidator u otras bibliotecas necesarias
            collection.AddValidatorsFromAssembly(Assembly.Load("Biblioteca.Application"));

            return collection;
        }
        public static IServiceCollection RegisterProviders(this IServiceCollection collection)
        {
            // Registrar cualquier proveedor específico que necesites

            return collection;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection collection)
        {
            // Registrar servicios específicos de la aplicación
            collection.AddTransient<UsuarioService>();
            collection.AddTransient<LibroService>();
            
            // Asegúrate de que el nombre del servicio sea correcto
            return collection;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
        {
            // Registrar repositorios específicos
            collection.AddTransient<IUsuarioRepository, UsuarioRepository>();
            collection.AddTransient<ILibroRepository, LibroRepository>();
            
            // Añade otros repositorios según sea necesario
            return collection;
        }
    }
}
