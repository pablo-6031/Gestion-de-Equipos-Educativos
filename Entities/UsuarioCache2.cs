﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class UsuarioCache2
    {
        public static int? IdUsuario { get; set; }
        public static string LoginName { get; set; }
        public static string Password { get; set; }
        public static string Nombre { get; set; }
        public static string Apellido { get; set; }
        public static string Cuil { get; set; }
        public static string Rol { get; set; }
        public static string Correo { get; set; }
        public static string Direccion { get; set; }
        public static byte[] Foto { get; set; }
        public static int IdInstitucion { get; set; }
    }
}
