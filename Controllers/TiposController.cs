using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APIfiestas.Models;
using APIfiestas.Models.request;
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
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipos()
        {
            return await _db.Tipo.ToListAsync();
        }

        /// <summary>
        /// Obtenemos todos los TiposBasico 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("obtenerTiposBasico")]
        public async Task<ActionResult<IEnumerable<TipoBasico>>> GetGruposBasico()
        {
            var tipos = await _db.Tipo.ToListAsync();
            if (tipos == null)
            {
                return NotFound();
            }
            List<TipoBasico> fi = new List<TipoBasico>();
            tipos.ForEach(delegate (Tipo tipo) { fi.Add(new TipoBasico(tipo)); });
            return fi;
        }

        /// <summary>
        /// Obtenemos un tipo por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Tipo>> GetTipo(int id)
        {
            var tipo = await _db.Tipo.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return tipo;
        }

        /// <summary>
        /// Nos permite añadir un tipo 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Pais>> Agregar([FromQuery] TipoAdd _tipoAdd)
        {
            Tipo _tipo = new Tipo();
            _tipo.Descripcion = _tipoAdd.nombre;
            _tipo.Falt = DateTime.Now;
            _tipo.Cusualt = null;
            _tipo.Fmod = null;
            _tipo.Cusumod = null;
            _db.Tipo.Add(_tipo);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetTipos", new { id = _tipo.Id }, _tipo);

        }

        /// <summary>
        /// Nos permite editar un tipo 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] TipoMod _tipoMod)
        {
            Tipo _tipo = await _db.Tipo.FindAsync(_tipoMod.id);
            if (_tipo == null)
            {
                return NotFound();
            }
            //_tipo.Id = _tipoMod.id;
            _tipo.Descripcion = _tipoMod.nombre;
            _tipo.Fmod = DateTime.Now;
            _tipo.Cusumod = null;
            _db.Entry(_tipo).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
                
            }
            
            return NoContent();

        }

        /// <summary>
        /// Eliminamos el tipo que le pasamos 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<Tipo>> Eliminar(int id)
        {
            
                var _tipo = await _db.Tipo.FindAsync(id);
                if (_tipo == null)
                {
                    return NotFound();
                }
                
                _db.Tipo.Remove(_tipo);
                await _db.SaveChangesAsync();
                return _tipo;
        }
    }
}