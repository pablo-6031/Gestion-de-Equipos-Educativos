using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UsuarioDao : ConnectionToSql
    {
        public DataTable ListarUsuarios()
        {
            DataTable listaUsuarios = new DataTable();
            using (var connection = GetConnection())
            {
                System.Data.SqlClient.SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "listarUsuarios";
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaUsuarios.Load(reader);
                    reader.Close();
                    return listaUsuarios;
                }
            }
        }

        public void AgregarUsuario(Usuario usuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@pLoginName", usuario.LoginName);
                    command.Parameters.AddWithValue("@pPassword", usuario.Password);
                    command.Parameters.AddWithValue("@pNombres", usuario.Nombre);
                    command.Parameters.AddWithValue("@pApellidos", usuario.Apellido);
                    command.Parameters.AddWithValue("@pIdRol", usuario.Rol);
                    command.Parameters.AddWithValue("@pCorreo", usuario.Correo);
                    command.Parameters.AddWithValue("@direccion", usuario.Direccion);
                    command.Parameters.AddWithValue("@pDNI", usuario.Dni);
                    command.Parameters.AddWithValue("@pFoto", usuario.Foto);
                    command.CommandText = "agregarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }
            }
        }

        public void EditarUsuario(Usuario usuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@pLoginName", usuario.LoginName);
                    command.Parameters.AddWithValue("@pPassword", usuario.Password);
                    command.Parameters.AddWithValue("@pNombres", usuario.Nombre);
                    command.Parameters.AddWithValue("@pApellidos", usuario.Apellido);
                    command.Parameters.AddWithValue("@pIdRol", usuario.Rol);
                    command.Parameters.AddWithValue("@pCorreo", usuario.Correo);
                    command.Parameters.AddWithValue("@direccion", usuario.Direccion);
                    command.Parameters.AddWithValue("@pDNI", usuario.Dni);
                    command.Parameters.AddWithValue("@pFoto", usuario.Foto);
                    command.CommandText = "editarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }


        public void EliminarUsuario(int IdUsuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@pIdUsuario", IdUsuario);
                    command.CommandText = "eliminarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ListarUsuarioFiltrado(string sp, string param)
        {
            string storProc = "listarUsuariosXApellido";
            string parametro = "@apellido";

            if (sp == "xLoginName")
            {
                storProc = "listarUsuariosXLoginName";
                parametro = "@loginName";
            }
            else
            {
                if (sp == "xApellido")
                {
                    storProc = "listarUsuariosXApellido";
                    parametro = "@apellido";
                }
                else
                {
                    storProc = "listarUsuariosXLegajo";
                    parametro = "@legajo";
                }
            }

            DataTable lstIdUsuariosFiltrados = new DataTable();

            using (var connection = GetConnection())
            {
                SqlDataReader leerFilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue(parametro, param);
                    command.CommandText = storProc;
                    command.CommandType = CommandType.StoredProcedure;
                    leerFilas = command.ExecuteReader();
                    lstIdUsuariosFiltrados.Load(leerFilas);
                    leerFilas.Close();
                    return lstIdUsuariosFiltrados;
                }
            }
        }

        //Metodo para validar el ingreso a la app
        public bool Login(string loginName, string password)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@pLoginName", loginName);
                    command.Parameters.AddWithValue("@pPassword", password);
                    command.CommandText = "select * from  USUARIOS where LoginName = @pLoginName and Password = @pPassword";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UsuarioCache.IdUsuario = reader.GetInt32(0);
                            UsuarioCache.LoginName = reader.GetString(5);
                            UsuarioCache.Password = reader.GetString(8);
                            UsuarioCache.Nombre = reader.GetString(2);
                            UsuarioCache.Apellido = reader.GetString(1);
                            UsuarioCache.Dni = reader.GetString(3);
                            UsuarioCache.Rol = reader.GetString(7);
                            UsuarioCache.Correo = reader.GetString(4);
                            UsuarioCache.Direccion = reader.GetString(6);
                            byte[] fotoDB = reader[10] != DBNull.Value ? (byte[])reader[10] : null;
                            UsuarioCache.Foto = fotoDB;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public byte[] ObtenerFoto(int idUsuario)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select Foto from USUARIOS where IdUsuario = @pIdUsuario";
                    command.Parameters.AddWithValue("@pIdUsuario", idUsuario);
                    command.CommandType = CommandType.Text;
                    leerfilas = command.ExecuteReader();
                    if (leerfilas.Read())
                    {
                        fotoDB = (byte[])leerfilas.GetValue(9);
                    }
                    leerfilas.Close();
                    return fotoDB;
                }
            }
        }
    }
}
