using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models
{
    public class GrupoBasico
    {
        public int id;
        public string descripcion;


        public GrupoBasico(Grupo grupo)
        {
            id = grupo.Id;
            descripcion = grupo.Descripcion;

        }


        //public string nomGrupo;

    }
}
