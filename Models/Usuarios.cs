using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("usuarios")]
    public partial class Usuarios
    {
        public Usuarios()
        {
            LoginsAttemps = new HashSet<LoginsAttemps>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("usuario", TypeName = "varchar(20)")]
        public string Usuario { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(128)")]
        public string Password { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("fbaj", TypeName = "datetime")]
        public DateTime? Fbaj { get; set; }
        [Column("id_tipo", TypeName = "int(11)")]
        public int IdTipo { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime? Fmod { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }
        [Required]
        [Column("email", TypeName = "varchar(255)")]
        public string Email { get; set; }

        [ForeignKey(nameof(IdTipo))]
        [InverseProperty(nameof(TipoUsu.Usuarios))]
        public virtual TipoUsu IdTipoNavigation { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<LoginsAttemps> LoginsAttemps { get; set; }
    }
}
