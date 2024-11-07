using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Curso { get; set; }
        public string Cuil { get; set; }
        public string Telefono { get; set; }
        public byte[] Foto { get; set; }
        public int IdInstitucion { get; set; }
    }
}
