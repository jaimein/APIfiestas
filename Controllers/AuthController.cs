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
            var user = this._palanciaContext.Usuarios.Where(u => u.Usuario.ToLower() == request.nombre.ToLower() && u.Password == request.passwod).FirstOrDefault();
            if(user == null)
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