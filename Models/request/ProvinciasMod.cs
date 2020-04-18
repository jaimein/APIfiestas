using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models.request
{
    public class ProvinciasMod
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int id_comunidad { get; set; }
    }
}
