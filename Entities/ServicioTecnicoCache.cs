using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class ServicioTecnicoCache
    {
        public static int IdServicioTecnico { get; set; }
        public static string Falla { get; set; }
        public static string Responsable { get; set; }
        public static DateTime FechaEnvio { get; set; }
        public static byte[] Foto { get; set; }
        public static int IdEquipo { get; set; }
    }
}
