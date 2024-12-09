using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DetallePrestamoEquipo
    {
        public int IdDetallePrestamoEquipo { get; set; }
        public int IdEquipo { get; set; }
        public int IdPrestamo { get; set; }
    }
}
