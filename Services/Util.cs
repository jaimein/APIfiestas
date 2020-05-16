using APIfiestas.Models;
using APIfiestas.Models.request;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIfiestas.Services
{
    public static class Util
    {
        public static string GetKey()
        {
            return  "ñahñlsha97tyñkljhav9'5gypwe7v'297ñ7";
        }
        public static AuthResponse CreateToken (Usuarios user, string keyEncript)
        {
            var tokenHanler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(keyEncript);
            var tokenDescriptor = new SecurityTokenDescriptor() 
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[] 
                    {
                        new Claim(ClaimTypes.Name, user.Usuario),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role,user.IdTipo.ToString()),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim("Guid", Guid.NewGuid().ToString())
                    }
                ),
                Expires = DateTime.Now.AddDays(1),
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            AuthResponse result = new AuthResponse() 
            {
                token = tokenHanler.WriteToken(tokenHanler.CreateToken(tokenDescriptor))
            };
            return result;
        }
    }
}
