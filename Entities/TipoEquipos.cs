using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoEquipos
    {
        public int IdTipoEquipo { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Detalle { get; set; }
        public byte[] Foto { get; set; }
    }
}
