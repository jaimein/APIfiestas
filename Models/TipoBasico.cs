using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models
{
    public class TipoBasico
    {
        public int id;
        public string descripcion;


        public TipoBasico(Tipo tipo)
        {
            id = tipo.Id;
            descripcion = tipo.Descripcion;

        }


        //public string nomGrupo;

    }
}
