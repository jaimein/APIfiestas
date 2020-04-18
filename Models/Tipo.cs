using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("tipo")]
    public partial class Tipo
    {
        public Tipo()
        {
            Fiesta = new HashSet<Fiesta>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("descripcion", TypeName = "varchar(45)")]
        public string Descripcion { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime? Fmod { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }

        [InverseProperty("IdTipoNavigation")]
        public virtual ICollection<Fiesta> Fiesta { get; set; }
    }
}
