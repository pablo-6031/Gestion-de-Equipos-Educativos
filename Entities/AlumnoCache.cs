using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class AlumnoCache
    {
        public static int IdAlumno { get; set; }
        public static string Apellidos { get; set; }
        public static string Nombres { get; set; }
        public static string Curso { get; set; }
        public static string Cuil { get; set; }
        public static string Telefono { get; set; }
        public static byte[] Foto { get; set; }
        public static int IdInstitucion { get; set; }
    }
}
