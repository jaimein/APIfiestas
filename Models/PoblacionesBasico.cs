using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models
{
    public class PoblacionesBasico
    {
        public int id;
        public string nombre;
        public int idProvincia;

        public PoblacionesBasico(Poblaciones poblaciones)
        {
            id = poblaciones.Id;
            nombre = poblaciones.Nombre;
            idProvincia = poblaciones.IdProvincia;
        }
    }
}
