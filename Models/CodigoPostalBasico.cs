using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models
{
    public class CodigoPostalBasico
    {
        public int id;
        public decimal cp;
        public string calle;
        public int idPoblacion;

        public CodigoPostalBasico(CodigoPostal codigoPostal)
        {
            id = codigoPostal.Id;
            cp = codigoPostal.Cp;
            calle = codigoPostal.Calle;
            idPoblacion = codigoPostal.IdPoblacion;
        }
    }
}
