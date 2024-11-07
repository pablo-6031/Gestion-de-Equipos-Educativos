using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Institucion
    {
        public int IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public string Cue { get; set; }
        public string Turno { get; set; }
        public string Director { get; set; }
        public string Nivel { get; set; }
        public string Calle { get; set; }
        public string NumeroCalle { get; set; }
        public string Barrio { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string region { get; set; }
        public string Telefono { get; set; }
    }
}
