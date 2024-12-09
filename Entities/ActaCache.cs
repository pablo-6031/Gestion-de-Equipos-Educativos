using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class ActaCache
    {
        public static int IdActa { get; set; }
        public static string NumeroActa { get; set; }
        public static string Estado { get; set; }
        public static DateTime FechaEntrega { get; set; }
        public static string Responsable { get; set; }
        public static byte[] Foto { get; set; }
        public static int IdProveedor { get; set; }
        public static int IdInstitucion { get; set; }
    }
}
