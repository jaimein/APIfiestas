using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("pais")]
    public partial class Pais
    {
        public Pais()
        {
            Comunidades = new HashSet<Comunidades>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("nombre", TypeName = "varchar(50)")]
        public string Nombre { get; set; }
        [Required]
        [Column("cod", TypeName = "varchar(2)")]
        public string Cod { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime? Fmod { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }

        [InverseProperty("IdPaisNavigation")]
        public virtual ICollection<Comunidades> Comunidades { get; set; }
    }
}
