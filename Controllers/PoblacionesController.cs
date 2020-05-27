using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIfiestas.Models;
using APIfiestas.Models.request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfiestas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoblacionesController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public PoblacionesController(PalanciaContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Obtenemos todos los Poblaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poblaciones>>> GetPoblaciones()
        {
            return await _db.Poblaciones.ToListAsync();
        }

        /// <summary>
        /// Obtenemos un poblaciones por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Poblaciones>> Getpoblaciones(int id)
        {
            var poblaciones = await _db.Poblaciones.FindAsync(id);
            if (poblaciones == null)
            {
                return NotFound();
            }
            return poblaciones;
        }

        /// <summary>
        /// Obtenemos un poblaciones por el id de provincia
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("poblacionesByProvincia")]
        public async Task<ActionResult<IEnumerable<PoblacionesBasico>>> GetpoblacionesByProvincia(int idProvincia)
        {
            var poblaciones = await _db.Poblaciones.AsNoTracking()
                                                    .Where(p => p.IdProvincia == idProvincia)
                                                    ?.Select(x => new PoblacionesBasico(x))
                                                    .ToListAsync();
            if (poblaciones == null)
            {
                return NotFound();
            }
            return poblaciones;
        }

        /// <summary>
        /// Nos permite añadir un poblaciones 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Pais>> Agregar([FromQuery] PoblacionesAdd _poblacionesAdd)
        {
            Poblaciones _poblaciones = new Poblaciones();
            _poblaciones.Nombre = _poblacionesAdd.nombre;
            _poblaciones.IdProvincia = _poblacionesAdd.id_provincia;
            _poblaciones.Falt = DateTime.Now;
            _poblaciones.Cusualt = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _poblaciones.Fmod = DateTime.Now;
            _poblaciones.Cusumod = null;
            _db.Poblaciones.Add(_poblaciones);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Getpoblaciones", new { id = _poblaciones.Id }, _poblaciones);

        }

        /// <summary>
        /// Nos permite editar un poblaciones 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] PoblacionesMod _poblacionesMod)
        {
            Poblaciones _poblaciones = await _db.Poblaciones.FindAsync(_poblacionesMod.id);
            if (_poblaciones == null)
            {
                return NotFound();
            }
            //_poblaciones.Id = _poblacionesMod.id;
            _poblaciones.Nombre = _poblacionesMod.nombre;
            _poblaciones.IdProvincia = _poblacionesMod.id_provincia;
            _poblaciones.Fmod = DateTime.Now;
            _poblaciones.Cusumod = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _db.Entry(_poblaciones).State = EntityState.Modified;
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
        /// Eliminamos el poblaciones que le pasamos 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<Poblaciones>> Eliminar(int id)
        {

            var _poblaciones = await _db.Poblaciones.FindAsync(id);
            if (_poblaciones == null)
            {
                return NotFound();
            }

            _db.Poblaciones.Remove(_poblaciones);
            await _db.SaveChangesAsync();
            return _poblaciones;
        }
    }
}