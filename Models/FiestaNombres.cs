using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models
{
    public class FiestaNombres
    {
        public int id;
        public DateTime fecha;
        public string nombreGrupo;
        public string nombreTipo;
        public string Localidad;
        public string zona;

        public FiestaNombres(Fiesta fiesta)
        {
            id = fiesta.Id;
            fecha = fiesta.Fecha;
            nombreTipo = fiesta.IdTipoNavigation.Descripcion;
            nombreGrupo = fiesta.IdGrupoNavigation.Descripcion;
            Localidad = fiesta.IdCodigoPostalNavigation.IdPoblacionNavigation.Nombre;
            zona = fiesta.IdCodigoPostalNavigation.Calle;
        }


        //public string nomGrupo;

    }
}
