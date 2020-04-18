using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("tipo_usu")]
    public partial class TipoUsu
    {
        public TipoUsu()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("description", TypeName = "varchar(50)")]
        public string Description { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime? Fmod { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }

        [InverseProperty("IdTipoNavigation")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
