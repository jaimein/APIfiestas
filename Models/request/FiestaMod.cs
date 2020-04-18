using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models.request
{
    public class FiestaMod
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public int id_grupo { get; set; }
        public int id_tipo { get; set; }
        public int id_codigo_postal { get; set; }
    }
}
