using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Equipo
    {
        public int IdEquipo { get; set; }
        public string Tipo { get; set; }
        public string NumSerie { get; set; }
        public string Proveedor { get; set; }
        public string Detalle { get; set; }
        public byte[] Foto { get; set; }
    }
}
