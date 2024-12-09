using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class TipoEquipoCache
    {
        public static int? IdTipoEquipo { get; set; }
        public static string Tipo { get; set; }
        public static string Marca { get; set; }
        public static string Modelo { get; set; }
        public static string Detalle { get; set; }
        public static byte[] Foto { get; set; }
    }
}
