using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ServicioTecnico
    {
        public int IdServicioTecnico { get; set; }
        public string Falla { get; set; }
        public DateTime FechaEnvio { get; set; }
        public byte[] Foto { get; set; }
        public int IdEquipo { get; set; }
    }
}
