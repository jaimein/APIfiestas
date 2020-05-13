using APIfiestas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Services
{
    public class FiestasService
    {
        private readonly PalanciaContext _PalanciaContext;

        public List<Fiesta> ObtenerTodos()
        {
            var resultado = _PalanciaContext.Fiesta.ToList();
            return resultado;
        }
    }
}
