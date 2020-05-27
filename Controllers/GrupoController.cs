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
    public class GrupoController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public GrupoController(PalanciaContext db)
        {
            this._db = db;
        }
        /// <summary>
        /// Obtenemos todos los Grupos 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grupo>>> GetGrupos()
        {
            return await _db.Grupo.ToListAsync();
        }

        /// <summary>
        /// Obtenemos todos los GruposBasico
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("obtenerGruposBasico")]
        public async Task<ActionResult<IEnumerable<GrupoBasico>>> GetGruposBasico()
        {
            var grupos = await _db.Grupo.ToListAsync();
            if(grupos == null)
            {
                return NotFound();
            }
            List<GrupoBasico> fi = new List<GrupoBasico>();
            grupos.ForEach(delegate (Grupo grupo) { fi.Add(new GrupoBasico(grupo)); });
            return fi;
        }

        /// <summary>
        /// Obtenemos un grupo por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Grupo>> Getgrupo(int id)
        {
            var grupo = await _db.Grupo.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            return grupo;
        }

        /// <summary>
        /// Nos permite añadir un grupo 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Pais>> Agregar(GrupoAdd _grupoAdd)
        {
            Grupo _grupo = new Grupo();
            _grupo.Descripcion = _grupoAdd.nombre;
            _grupo.Falt = DateTime.Now;
            _grupo.Cusualt = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _grupo.Fmod = null;
            _grupo.Cusumod = null;
            _db.Grupo.Add(_grupo);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Getgrupos", new { id = _grupo.Id }, _grupo);

        }

        /// <summary>
        /// Nos permite editar un grupo 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] GrupoMod _grupoMod)
        {
            Grupo _grupo = await _db.Grupo.FindAsync(_grupoMod.id);
            if (_grupo == null)
            {
                return NotFound();
            }
            //_grupo.Id = _grupoMod.id;
            _grupo.Descripcion = _grupoMod.nombre;
            _grupo.Fmod = DateTime.Now;
            _grupo.Cusumod = User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).Select(a => a.Value).FirstOrDefault();
            _db.Entry(_grupo).State = EntityState.Modified;
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
        /// Eliminamos el grupo que le pasamos 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<Grupo>> Eliminar(int id)
        {

            var _grupo = await _db.Grupo.FindAsync(id);
            if (_grupo == null)
            {
                return NotFound();
            }

            _db.Grupo.Remove(_grupo);
            await _db.SaveChangesAsync();
            return _grupo;
        }
    }
}