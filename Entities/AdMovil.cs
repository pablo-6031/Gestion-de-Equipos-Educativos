using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AdMovil
    {
        public int IdAdMovil { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public string Descripcion { get; set; }
        public byte[] Foto { get; set; }
    }
}
