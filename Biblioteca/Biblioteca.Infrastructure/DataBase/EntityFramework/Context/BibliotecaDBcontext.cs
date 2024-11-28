using Biblioteca.Infrastructure.DataBase.EntityFramework.Context.Biblioteca;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Context
{
    public class BibliotecaDBcontext:DbContext
    {
        // DbSets for entities
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        
        public DbSet<LibroEntity> Libros { get; set; }
        





        // Constructor for dependency injection
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=190.11.64.59;Database=RequestService;User ID=sa;Password=SuintMSSQL2#;Trusted_Connection= false;MultipleActiveResultSets=true;");
        //local DB
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=DESKTOP-SDKVUQH;Database=Biblioteca;Integrated Security=True;MultipleActiveResultSets=true;");

        public BibliotecaDBcontext(DbContextOptions<BibliotecaDBcontext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder entity)
        {
            base.OnModelCreating(entity);
            entity.ConfigureUsuario();
            entity.ConfigureLibro();
            //add cofigurations
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {

                switch (entry.State)

                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = GetCurrentUserId();

                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = GetCurrentUserId();
                        break;

                    case EntityState.Modified:
                        entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                        entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = GetCurrentUserId();
                        break;
                }
            }
        }

        private int GetCurrentUserId()
        {
            // Replace with actual user retrieval logic
            return 106;
        }



    }
}
