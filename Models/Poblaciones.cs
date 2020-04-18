using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("poblaciones")]
    public partial class Poblaciones
    {
        public Poblaciones()
        {
            CodigoPostal = new HashSet<CodigoPostal>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("nombre", TypeName = "varchar(100)")]
        public string Nombre { get; set; }
        [Column("id_provincia", TypeName = "int(11)")]
        public int IdProvincia { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime? Fmod { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }

        [ForeignKey(nameof(IdProvincia))]
        [InverseProperty(nameof(Provincias.Poblaciones))]
        public virtual Provincias IdProvinciaNavigation { get; set; }
        [InverseProperty("IdPoblacionNavigation")]
        public virtual ICollection<CodigoPostal> CodigoPostal { get; set; }
    }
}
