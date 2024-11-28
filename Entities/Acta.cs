using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Acta
    {
        public int IdActa { get; set; }
        public string NumeroActa { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Responsable { get; set; }
        public byte[] Foto { get; set; }
        public int IdProveedor { get; set; }
        public int IdInstitucion { get; set; }
    }
}
