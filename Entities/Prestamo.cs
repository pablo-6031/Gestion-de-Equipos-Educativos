﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public string Dni { get; set; }
        public string Funcion { get; set; }

    }
}
