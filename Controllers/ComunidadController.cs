using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIfiestas.Models;
using APIfiestas.Models.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfiestas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunidadController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public ComunidadController(PalanciaContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Obtenemos todos los Comunidades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comunidades>>> GetComunidadess()
        {
            return await _db.Comunidades.ToListAsync();
        }

        /// <summary>
        /// Obtenemos un comunidades por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Comunidades>> Getcomunidades(int id)
        {
            var comunidades = await _db.Comunidades.FindAsync(id);
            if (comunidades == null)
            {
                return NotFound();
            }
            return comunidades;
        }

        /// <summary>
        /// Nos permite añadir un comunidades 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Comunidades>> Agregar([FromQuery] ComunidadesAdd _comunidadesAdd)
        {
            Comunidades _comunidades = new Comunidades();
            _comunidades.Nombre = _comunidadesAdd.nombre;
            _comunidades.IdPais = _comunidadesAdd.id_pais;
            _comunidades.Falt = DateTime.Now;
            _comunidades.Cusualt = null;
            _comunidades.Fmod = DateTime.Now;
            _comunidades.Cusumod = null;
            _db.Comunidades.Add(_comunidades);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Getcomunidades", new { id = _comunidades.Id }, _comunidades);

        }

        /// <summary>
        /// Nos permite editar un comunidades 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] ComunidadesMod _comunidadesMod)
        {
            Comunidades _comunidades = await _db.Comunidades.FindAsync(_comunidadesMod.id);
            if (_comunidades == null)
            {
                return NotFound();
            }
            //_comunidades.Id = _comunidadesMod.id;
            _comunidades.Nombre = _comunidadesMod.nombre;
            _comunidades.IdPais = _comunidadesMod.id_pais;
            _comunidades.Fmod = DateTime.Now;
            _comunidades.Cusumod = null;
            _db.Entry(_comunidades).State = EntityState.Modified;
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
        /// Eliminamos el comunidades que le pasamos 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<Comunidades>> Eliminar(int id)
        {

            var _comunidades = await _db.Comunidades.FindAsync(id);
            if (_comunidades == null)
            {
                return NotFound();
            }

            _db.Comunidades.Remove(_comunidades);
            await _db.SaveChangesAsync();
            return _comunidades;
        }
    }
}