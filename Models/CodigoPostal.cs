using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("codigo_postal")]
    public partial class CodigoPostal
    {
        public CodigoPostal()
        {
            Fiesta = new HashSet<Fiesta>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("cp", TypeName = "decimal(5,0)")]
        public decimal Cp { get; set; }
        [Required]
        [Column("calle", TypeName = "varchar(100)")]
        public string Calle { get; set; }
        [Column("id_poblacion", TypeName = "int(11)")]
        public int IdPoblacion { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime Fmod { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }

        [ForeignKey(nameof(IdPoblacion))]
        [InverseProperty(nameof(Poblaciones.CodigoPostal))]
        public virtual Poblaciones IdPoblacionNavigation { get; set; }
        [InverseProperty("IdCodigoPostalNavigation")]
        public virtual ICollection<Fiesta> Fiesta { get; set; }
    }
}
