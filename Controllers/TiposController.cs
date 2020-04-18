using System;
using System.Collections.Generic;
using System.Linq;
using APIfiestas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Palancia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TiposController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public TiposController(PalanciaContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Obtenemos todos los tipos 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Tipo> Get()
        {
            return _db.Tipo.ToList();
        }

        /// <summary>
        /// Nos permite añadir un tipo 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agregar")]
        public IActionResult Agregar([FromBody] Tipo _tipo)
        {
            try
            {
                _db.Tipo.Add(_tipo);
                _db.SaveChanges();
                return Ok();
            } catch (Exception error)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Nos permite editar un tipo 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public IActionResult Editar([FromBody] Tipo _tipo)
        {
            try
            {
                _db.Entry(_tipo).State = EntityState.Modified;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Eliminamos el tipo que le pasamos 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var _tipo = _db.Tipo.FirstOrDefault(x => x.Id == id);
                if (_tipo == null)
                {
                    return NotFound();
                }
                //_db.Tipo.Remove(tipo);
                _db.Tipo.Remove(_tipo);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest();
            }
        }
    }
}