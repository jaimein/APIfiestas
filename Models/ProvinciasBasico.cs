using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models
{
    public class ProvinciasBasico
    {
        public int id;
        public string nombre;
        public int idComunidad;

        public ProvinciasBasico(Provincias provincias)
        {
            id = provincias.Id;
            nombre = provincias.Nombre;
            idComunidad = provincias.IdComunidad;

        }
    }
}
