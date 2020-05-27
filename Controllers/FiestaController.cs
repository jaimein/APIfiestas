using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIfiestas.Models;
using APIfiestas.Models.request;
using APIfiestas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfiestas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiestaController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public FiestaController(PalanciaContext db)
        {
            this._db = db;            
        }

        /// <summary>
        /// Obtenemos todos los Fiesta 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fiesta>>> GetAllFiestas()
        {
            return await _db.Fiesta.ToListAsync();
        }

        /// <summary>
        /// Obtenemos todos los Fiesta solo nombres
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("obtenerFiestasSimple")]
        public async Task<ActionResult<IEnumerable<FiestaNombres>>> GetAllFiestasSimple()
        {
            var fiestas = await _db.Fiesta.AsNoTracking().Where(x => x.Fecha >= DateTime.Today)
                                            .Include( x=>x.IdTipoNavigation)
                                            .Include( g => g.IdGrupoNavigation)
                                            .Include(l => l.IdCodigoPostalNavigation.IdPoblacionNavigation)
                                            .OrderBy( o => o.Fecha)
                                            ?.Select (f => new FiestaNombres(f))
                                            .ToListAsync();
            if (fiestas == null)
            {
                return NotFound();
            }                       
            return fiestas;            
        }

        /// <summary>
        /// Obtenemos la Fiesta solo nombres por el id que le pasamos
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("obtenerFiestaSimple")]
        public async Task<ActionResult<FiestaNombres>> GetFiestaSimple(int id)
        {
            var fiestas = await _db.Fiesta.AsNoTracking().Where(x => x.Id == id)
                                            .Include(x => x.IdTipoNavigation)
                                            .Include(g => g.IdGrupoNavigation)
                                            .Include(l => l.IdCodigoPostalNavigation.IdPoblacionNavigation)
                                            ?.Select(f => new FiestaNombres(f))
                                            .FirstOrDefaultAsync();
            if (fiestas == null)
            {
                return NotFound();
            }
            return fiestas;
        }

        /// <summary>
        /// Obtenemos el numero de fiestas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("obtenerNumeroFiestas")]
        public async Task<ActionResult<IEnumerable<int>>> GetNextFiestas()
        {

            var numero =  _db.Fiesta.Where(x => x.Fecha.Date >= DateTime.Today.Date && x.Fecha.Date <= DateTime.Today.Date.AddDays(15)).GroupBy(d => d.Fecha.Date).Select(f => new { Fecha = f.Key, count = f.Count() });//ToListAsync();
            return Ok(numero);

        }

        /// <summary>
        /// Obtenemos un fiesta por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Fiesta>> Getfiesta(int id)
        {
            var fiesta = await _db.Fiesta.FindAsync(id);
            if (fiesta == null)
            {
                return NotFound();
            }
            return fiesta;
        }

        /// <summary>
        /// Obtenemos todos los Fiesta que estan por venir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("obtenerFiestas")]
        public async Task<ActionResult<IEnumerable<Fiesta>>> GetFiestas()
        {
            return await _db.Fiesta.Where(x => x.Fecha >= DateTime.Today).ToListAsync();
        }

        /// <summary>
        /// Nos permite añadir un fiesta 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Fiesta>> Agregar(FiestaAdd _fiestaAdd)
        {
            Fiesta _fiesta = new Fiesta();
            _fiesta.Fecha = _fiestaAdd.fecha;
            _fiesta.IdCodigoPostal = _fiestaAdd.id_codigo_postal;
            _fiesta.IdGrupo = _fiestaAdd.id_grupo;
            _fiesta.IdTipo = _fiestaAdd.id_tipo;
            _fiesta.Falt = DateTime.Now;
            _fiesta.Cusualt = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _fiesta.Fmod = DateTime.Now;
            _fiesta.Cusumod = null;
            _db.Fiesta.Add(_fiesta);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Getfiesta", new { id = _fiesta.Id }, _fiesta);

        }

        /// <summary>
        /// Nos permite editar un fiesta 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] FiestaMod _fiestaMod)
        {
            Fiesta _fiesta = await _db.Fiesta.FindAsync(_fiestaMod.id);
            if (_fiesta == null)
            {
                return NotFound();
            }
            //_fiesta.Id = _fiestaMod.id;
            _fiesta.IdCodigoPostal = _fiestaMod.id_codigo_postal;
            _fiesta.IdGrupo = _fiestaMod.id_grupo;
            _fiesta.IdTipo = _fiestaMod.id_tipo;
            _fiesta.Fmod = DateTime.Now;
            _fiesta.Cusumod = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _db.Entry(_fiesta).State = EntityState.Modified;
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
        /// Eliminamos el fiesta que le pasamos 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("eliminar")]
        public async Task<ActionResult<Fiesta>> Eliminar(int id)
        {

            var _fiesta = await _db.Fiesta.FindAsync(id);
            if (_fiesta == null)
            {
                return NotFound();
            }

            _db.Fiesta.Remove(_fiesta);
            await _db.SaveChangesAsync();
            return _fiesta;
        }
    }

}