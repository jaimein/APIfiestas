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
    public class PaisController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public PaisController(PalanciaContext db)
        {
            this._db = db;
        }
        /// <summary>
        /// Obtenemos todos los Paises 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaiss()
        {
            return await _db.Pais.ToListAsync();
        }

        /// <summary>
        /// Obtenemos un pais por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Pais>> Getpais(int id)
        {
            var pais = await _db.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            return pais;
        }

        /// <summary>
        /// Nos permite añadir un pais 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Pais>> Agregar([FromQuery] PaisAdd _paisAdd)
        {
            Pais _pais = new Pais();
            _pais.Nombre = _paisAdd.nombre;
            _pais.Cod = _paisAdd.cod;
            _pais.Falt = DateTime.Now;
            _pais.Cusualt = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _pais.Fmod = null;
            _pais.Cusumod = null;
            _db.Pais.Add(_pais);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Getpaiss", new { id = _pais.Id }, _pais);

        }

        /// <summary>
        /// Nos permite editar un pais 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] PaisMod _paisMod)
        {
            Pais _pais = await _db.Pais.FindAsync(_paisMod.id);
            if (_pais == null)
            {
                return NotFound();
            }
            //_pais.Id = _paisMod.id;
            _pais.Nombre = _paisMod.nombre;
            _pais.Cod = _paisMod.cod;
            _pais.Fmod = DateTime.Now;
            _pais.Cusumod = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _db.Entry(_pais).State = EntityState.Modified;
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
        /// Eliminamos el pais que le pasamos 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<Pais>> Eliminar(int id)
        {

            var _pais = await _db.Pais.FindAsync(id);
            if (_pais == null)
            {
                return NotFound();
            }

            _db.Pais.Remove(_pais);
            await _db.SaveChangesAsync();
            return _pais;
        }
    }
}