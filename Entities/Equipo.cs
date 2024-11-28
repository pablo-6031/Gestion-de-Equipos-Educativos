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
        public string NumSerie { get; set; }
        public string Matricula { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public string Destino { get; set; }
        public int IdTipoEquipo { get; set; }
        public int IdActa { get; set; }
    }
}
