using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Context.Biblioteca
{
    public static class UsuarioConfiguration
    {
        public static void ConfigureUsuario(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                // Configurar la tabla y esquema (opcional, ya definido en la anotación [Table])
                entity.ToTable("Usuario", "Biblioteca");

                // Configurar índice único en Email para asegurar que sea único
                entity.HasIndex(u => u.Email).IsUnique();

                // Configurar propiedades
                entity.Property(u => u.Nombre)
                      .IsRequired()
                      .HasMaxLength(50); // Longitud máxima de 50 caracteres

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100); // Longitud máxima de 100 caracteres

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(255); // Longitud máxima para contraseña cifrada

                
            });
        }
    }
}
