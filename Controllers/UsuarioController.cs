using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class UsuarioController
    {
        private UsuarioDao usuarioDao = new UsuarioDao();

        public DataTable listaUsuarios()
        {
            return usuarioDao.ListarUsuarios();
        }

        public DataTable FiltrarUsuarios(string texto)
        {
            return usuarioDao.FiltrarUsuarios(texto);
        }

        

        public string AgregarUsuario(Usuario usuario)
        {
            return usuarioDao.AgregarUsuario(usuario);
        }

        public string EditarUsuario(Usuario usuario)
        {
            return usuarioDao.EditarUsuario(usuario);
        }

        public void eliminarUsuario(int id)
        {
            usuarioDao.EliminarUsuario(id);
        }

        public byte[] obtenerFoto(int id)
        {
            return usuarioDao.ObtenerFoto(id);
        }
        public bool login(string loginName, string password)
        {
            return usuarioDao.Login(loginName, password);
        }

    }
}
