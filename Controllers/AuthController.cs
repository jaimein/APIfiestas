using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using APIfiestas.Models;
using APIfiestas.Models.request;
using APIfiestas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIfiestas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PalanciaContext _palanciaContext;
        public AuthController(PalanciaContext palanciaContext)
        {
            this._palanciaContext = palanciaContext;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthRequest request)
        {
            var user = this._palanciaContext.Usuarios.Where(u => u.Usuario.ToLower() == request.nombre.ToLower() && u.Password == request.password).FirstOrDefault();
            if(user == null)
            {
                return Unauthorized();
            }

            
            return Ok(Util.CreateToken(user, Util.GetKey()));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> Crear(AuthRequest_new request)
        {
            Usuarios _usu = new Usuarios();
            _usu.Cusualt = request.nombre;
            _usu.Email = request.email;
            _usu.Password = request.password;
            _usu.Usuario = request.nombre;
            _usu.IdTipo = 1;
            _palanciaContext.Usuarios.Add(_usu);
            await _palanciaContext.SaveChangesAsync();
            var user = this._palanciaContext.Usuarios.Where(u => u.Usuario.ToLower() == request.nombre.ToLower() && u.Password == request.password).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }


            return Ok(Util.CreateToken(user, Util.GetKey()));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok(true);
        }
    }
}