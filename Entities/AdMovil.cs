﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AdMovil
    {
        public int IdAdMovil { get; set; }
        public string Matricula { get; set; }
        public string NumSerie { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public int IdActa { get; set; }
        public int IdTipoEquipo { get; set; }
    }
}
