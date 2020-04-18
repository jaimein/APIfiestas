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
    public class UsuariosController : ControllerBase
    {
        private readonly PalanciaContext _db;
        public UsuariosController(PalanciaContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Obtenemos todos los Usuarios 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarioss()
        {
            return await _db.Usuarios.ToListAsync();
        }

        /// <summary>
        /// Obtenemos un usuarios por el id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Usuarios>> Getusuarios(int id)
        {
            var usuarios = await _db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return usuarios;
        }

        /// <summary>
        /// Nos permite añadir un usuarios 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult<Usuarios>> Agregar([FromQuery] UsuariosAdd _usuariosAdd)
        {
            Usuarios _usuarios = new Usuarios();
            _usuarios.Usuario = _usuariosAdd.usuario;
            _usuarios.Password = _usuariosAdd.password;
            _usuarios.IdTipo = _usuariosAdd.id_tipo;
            _usuarios.Email = _usuariosAdd.email;
            _usuarios.Falt = DateTime.Now;
            _usuarios.Cusualt = null;
            _usuarios.Fmod = DateTime.Now;
            _usuarios.Cusumod = null;
            _db.Usuarios.Add(_usuarios);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Getusuarios", new { id = _usuarios.Id }, _usuarios);

        }

        /// <summary>
        /// Nos permite editar un usuarios 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromQuery] UsuariosMod _usuariosMod)
        {
            Usuarios _usuarios = await _db.Usuarios.FindAsync(_usuariosMod.id);
            if (_usuarios == null)
            {
                return NotFound();
            }
            //_usuarios.Id = _usuariosMod.id;
            _usuarios.Usuario = _usuariosMod.usuario;
            _usuarios.Password = _usuariosMod.password;
            _usuarios.IdTipo = _usuariosMod.id_tipo;
            _usuarios.Email = _usuariosMod.email;
            _usuarios.Fmod = DateTime.Now;
            _usuarios.Cusumod = null;
            _db.Entry(_usuarios).State = EntityState.Modified;
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
        /// Eliminamos el usuarios que le pasamos 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<Usuarios>> Eliminar(int id)
        {

            var _usuarios = await _db.Usuarios.FindAsync(id);
            if (_usuarios == null)
            {
                return NotFound();
            }

            _db.Usuarios.Remove(_usuarios);
            await _db.SaveChangesAsync();
            return _usuarios;
        }
    }
}