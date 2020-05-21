using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class ProvinciaController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public ProvinciaController(PalanciaContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Obtenemos todos los Provinciass 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincias>>> GetProvinciass()
        {
            return await _db.Provincias.ToListAsync();
        }

        /// <summary>
        /// Obtenemos un provincias por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Provincias>> Getprovincias(int id)
        {
            var provincias = await _db.Provincias.FindAsync(id);
            if (provincias == null)
            {
                return NotFound();
            }
            return provincias;
        }

        /// <summary>
        /// Obtenemos un provincias de un id de comunidades 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("provinciasByComunidad")]
        public async Task<ActionResult<IEnumerable<ProvinciasBasico>>> GetprovinciasByComunidad(int idProvincia)
        {
            var provincias = await _db.Provincias.AsNoTracking()
                                                 .Where(p => p.IdComunidad == idProvincia)
                                                 ?.Select( x => new ProvinciasBasico(x))
                                                 .ToListAsync();
            if (provincias == null)
            {
                return NotFound();
            }
            return provincias;
        }

        /// <summary>
        /// Nos permite añadir un provincias 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Provincias>> Agregar([FromQuery] ProvinciasAdd _provinciasAdd)
        {
            Provincias _provincias = new Provincias();
            _provincias.Nombre = _provinciasAdd.nombre;
            _provincias.IdComunidad = _provinciasAdd.id_comunidad;
            _provincias.Falt = DateTime.Now;
            _provincias.Cusualt = null;
            _provincias.Fmod = DateTime.Now;
            _provincias.Cusumod = null;
            _db.Provincias.Add(_provincias);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Getprovinciass", new { id = _provincias.Id }, _provincias);

        }

        /// <summary>
        /// Nos permite editar un provincias 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] ProvinciasMod _provinciasMod)
        {
            Provincias _provincias = await _db.Provincias.FindAsync(_provinciasMod.id);
            if (_provincias == null)
            {
                return NotFound();
            }
            //_provincias.Id = _provinciasMod.id;
            _provincias.Nombre = _provinciasMod.nombre;
            _provincias.IdComunidad = _provinciasMod.id_comunidad;
            _provincias.Fmod = DateTime.Now;
            _provincias.Cusumod = null;
            _db.Entry(_provincias).State = EntityState.Modified;
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
        /// Eliminamos el provincias que le pasamos 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<Provincias>> Eliminar(int id)
        {

            var _provincias = await _db.Provincias.FindAsync(id);
            if (_provincias == null)
            {
                return NotFound();
            }

            _db.Provincias.Remove(_provincias);
            await _db.SaveChangesAsync();
            return _provincias;
        }
    }
}