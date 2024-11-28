using Biblioteca.Domain.Dtos.Biblioteca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca
{
    [Table("Usuario", Schema = "Biblioteca")]

    public class UsuarioEntity: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
       [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        public string Email {  get; set; }

        [Required]
        public string Password { get; set; }

        public RolEnum Rol {  get; set; }
    }
}
