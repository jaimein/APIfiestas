using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("comunidades")]
    public partial class Comunidades
    {
        public Comunidades()
        {
            Provincias = new HashSet<Provincias>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("nombre", TypeName = "varchar(100)")]
        public string Nombre { get; set; }
        [Column("id_pais", TypeName = "int(11)")]
        public int IdPais { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime Fmod { get; set; }

        [ForeignKey(nameof(IdPais))]
        [InverseProperty(nameof(Pais.Comunidades))]
        public virtual Pais IdPaisNavigation { get; set; }
        [InverseProperty("IdComunidadNavigation")]
        public virtual ICollection<Provincias> Provincias { get; set; }
    }
}
