using APIfiestas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Services
{
    public class TiposService
    {
        private readonly PalanciaContext _PalanciaContext;

        public TiposService(PalanciaContext palanciaContext)
        {
            _PalanciaContext = palanciaContext;
        }

        public List<Tipo> ObtenerTodos()
        {
            var resultado = _PalanciaContext.Tipo.ToList();
            return resultado;
        }

        public Boolean Agregar(Tipo _tipo)
        {
            try
            {
                _PalanciaContext.Tipo.Add(_tipo);
                _PalanciaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Boolean EditarTipo(Tipo _tipo)
        {
            try
            {
                _PalanciaContext.Entry(_tipo).State = EntityState.Modified;
                _PalanciaContext.SaveChanges();
                
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }

        public Boolean EliminarTipo(int idTipo)
        {
            try
            {
                var _tipo = _PalanciaContext.Tipo.FirstOrDefault(x => x.Id == idTipo);
                if (_tipo == null)
                {
                    return false;
                }
                //_db.Tipo.Remove(tipo);
                _PalanciaContext.Tipo.Remove(_tipo);
                _PalanciaContext.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
    }

}
