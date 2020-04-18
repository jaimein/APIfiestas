using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    [Table("fiesta")]
    public partial class Fiesta
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("fecha", TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        [Column("falt", TypeName = "datetime")]
        public DateTime Falt { get; set; }
        [Column("cusualt", TypeName = "varchar(20)")]
        public string Cusualt { get; set; }
        [Column("id_grupo", TypeName = "int(11)")]
        public int IdGrupo { get; set; }
        [Column("id_tipo", TypeName = "int(11)")]
        public int IdTipo { get; set; }
        [Column("id_codigo_postal", TypeName = "int(11)")]
        public int IdCodigoPostal { get; set; }
        [Column("fmod", TypeName = "datetime")]
        public DateTime? Fmod { get; set; }
        [Column("cusumod", TypeName = "varchar(20)")]
        public string Cusumod { get; set; }

        [ForeignKey(nameof(IdCodigoPostal))]
        [InverseProperty(nameof(CodigoPostal.Fiesta))]
        public virtual CodigoPostal IdCodigoPostalNavigation { get; set; }
        [ForeignKey(nameof(IdGrupo))]
        [InverseProperty(nameof(Grupo.Fiesta))]
        public virtual Grupo IdGrupoNavigation { get; set; }
        [ForeignKey(nameof(IdTipo))]
        [InverseProperty(nameof(Tipo.Fiesta))]
        public virtual Tipo IdTipoNavigation { get; set; }
    }
}
