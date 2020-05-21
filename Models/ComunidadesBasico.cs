using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfiestas.Models
{
    public class ComunidadesBasico
    {

        public int id;
        public string nombre;
        public int idpais;


        public ComunidadesBasico(Comunidades comunidades)
        {
            id = comunidades.Id;
            nombre = comunidades.Nombre;
            idpais = comunidades.IdPais;

        }


        //public string nomGrupo;

    }
}
