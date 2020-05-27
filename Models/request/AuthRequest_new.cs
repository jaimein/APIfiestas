﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIfiestas.Models.request
{
    public class AuthRequest_new
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }

    }
}
