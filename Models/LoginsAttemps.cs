using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("logins_attemps")]
    public partial class LoginsAttemps
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("time", TypeName = "datetime")]
        public DateTime Time { get; set; }
        [Column("id_usuario", TypeName = "int(11)")]
        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.LoginsAttemps))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
