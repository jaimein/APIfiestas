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
    public class CodigoPostalController : ControllerBase
    {

        private readonly PalanciaContext _db;
        public CodigoPostalController(PalanciaContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Obtenemos todos los CodigoPostal 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodigoPostal>>> GetCodigoPostals()
        {
            return await _db.CodigoPostal.ToListAsync();
        }

        /// <summary>
        /// Obtenemos un codigoPostal por el id 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<CodigoPostal>> GetcodigoPostal(int id)
        {
            var codigoPostal = await _db.CodigoPostal.FindAsync(id);
            if (codigoPostal == null)
            {
                return NotFound();
            }
            return codigoPostal;
        }

        /// <summary>
        /// Nos permite añadir un codigoPostal 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<CodigoPostal>> Agregar([FromQuery] CodigoPostalAdd _codigoPostalAdd)
        {
            CodigoPostal _codigoPostal = new CodigoPostal();
            _codigoPostal.Cp = _codigoPostalAdd.cp;
            _codigoPostal.Calle = _codigoPostalAdd.calle;
            _codigoPostal.IdPoblacion = _codigoPostalAdd.id_poblacion;
            _codigoPostal.Falt = DateTime.Now;
            _codigoPostal.Cusualt = null;
            _codigoPostal.Fmod = DateTime.Now;
            _codigoPostal.Cusumod = null;
            _db.CodigoPostal.Add(_codigoPostal);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetcodigoPostal", new { id = _codigoPostal.Id }, _codigoPostal);

        }

        /// <summary>
        /// Nos permite editar un codigoPostal 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] CodigoPostalMod _codigoPostalMod)
        {
            CodigoPostal _codigoPostal = await _db.CodigoPostal.FindAsync(_codigoPostalMod.id);
            if (_codigoPostal == null)
            {
                return NotFound();
            }
            //_codigoPostal.Id = _codigoPostalMod.id;
            _codigoPostal.Cp = _codigoPostalMod.cp;
            _codigoPostal.Calle = _codigoPostalMod.calle;
            _codigoPostal.IdPoblacion = _codigoPostalMod.id_poblacion;
            _codigoPostal.Fmod = DateTime.Now;
            _codigoPostal.Cusumod = null;
            _db.Entry(_codigoPostal).State = EntityState.Modified;
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
        /// Eliminamos el codigoPostal que le pasamos 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<CodigoPostal>> Eliminar(int id)
        {

            var _codigoPostal = await _db.CodigoPostal.FindAsync(id);
            if (_codigoPostal == null)
            {
                return NotFound();
            }

            _db.CodigoPostal.Remove(_codigoPostal);
            await _db.SaveChangesAsync();
            return _codigoPostal;
        }
    }
}