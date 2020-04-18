using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models.request
{
    public class UsuariosAdd
    {
        public string usuario { get; set; }
        public string password { get; set; }
        public int id_tipo { get; set; }
        public string email { get; set; }
    }
}
