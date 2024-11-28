using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Context.Biblioteca
{
    public static class LibroConfiguration
    {
        public static void ConfigureLibro(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibroEntity>(entity =>
            {
                // Configurar la tabla y el esquema
                entity.ToTable("Libros", "Biblioteca");

                // Configurar las propiedades
                entity.Property(l => l.Codigo)
                    .IsRequired() 
                    .HasMaxLength(50); 

                entity.Property(l => l.Titulo)
                    .IsRequired() 
                    .HasMaxLength(200);

                entity.Property(l => l.Autor)
                    .IsRequired() 
                    .HasMaxLength(150); 

                entity.Property(l => l.UrlImagen)
                    .HasMaxLength(500); 
                
                entity.HasKey(l => l.LibroId);

                entity.HasIndex(l => l.Codigo).IsUnique(); // Si se desea que Código sea único
            });
        }
    }
}