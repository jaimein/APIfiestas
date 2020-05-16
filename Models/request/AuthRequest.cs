using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models.request
{
    public class AuthRequest
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string passwod { get; set; }
    }
}
