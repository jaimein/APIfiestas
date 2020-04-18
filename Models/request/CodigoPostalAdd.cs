using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models.request
{
    public class CodigoPostalAdd
    {
        public int cp { get; set; }
        public string calle { get; set; }
        public int id_poblacion { get; set; }
    }
}
