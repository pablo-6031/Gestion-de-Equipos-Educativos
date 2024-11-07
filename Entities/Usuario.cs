using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cuil { get; set; }
        public string Rol { get; set; }
        public string Correo { get; set; }
        public byte[] Foto { get; set; }
    }
}
