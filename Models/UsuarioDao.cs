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
                    command.CommandText = "sp_ListarUsuarios";
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
                    command.Parameters.AddWithValue("@loginName", usuario.LoginName);
                    command.Parameters.AddWithValue("@password", usuario.Password);
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@rol", usuario.Rol);
                    command.Parameters.AddWithValue("@correo", usuario.Correo);
                    command.Parameters.AddWithValue("@direccion", usuario.Direccion);
                    command.Parameters.AddWithValue("@dni", usuario.Dni);
                    command.Parameters.AddWithValue("@foto", usuario.Foto);
                    command.CommandText = "sp_AgregarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }
            }
        }

        public void EditarUsuario(Usuario usu)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idUsuario", usu.IdUsuario);
                    command.Parameters.AddWithValue("@loginName", usu.LoginName);
                    command.Parameters.AddWithValue("@password", usu.Password);
                    command.Parameters.AddWithValue("@nombre", usu.Nombre);
                    command.Parameters.AddWithValue("@apellido", usu.Apellido);
                    command.Parameters.AddWithValue("@dni", usu.Dni);
                    command.Parameters.AddWithValue("@rol", usu.Rol);
                    command.Parameters.AddWithValue("@correo", usu.Correo);
                    command.Parameters.AddWithValue("@direccion", usu.Direccion);
                    command.Parameters.AddWithValue("@foto", usu.Foto);
                    command.CommandText = "sp_EditarUsuario";
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
                    command.Parameters.AddWithValue("@idUsuario", IdUsuario);
                    command.CommandText = "sp_EliminarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ListarUsuarioFiltrado(string sp, string param)
        {
            string storProc = "sp_ListarUsuariosXApellido";
            string parametro = "@apellido";

            if (sp == "xLoginName")
            {
                storProc = "sp_ListarUsuariosXLoginName";
                parametro = "@loginName";
            }
            else
            {
                if (sp == "xApellido")
                {
                    storProc = "sp_ListarUsuariosXApellido";
                    parametro = "@apellido";
                }
                else
                {
                    storProc = "sp_ListarUsuariosXLegajo";
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
                    command.Parameters.AddWithValue("@loginName", loginName);
                    command.Parameters.AddWithValue("@password", password);
                    command.CommandText = "select * from  USUARIOS where LoginName = @loginName and Password = @password";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UsuarioCache.IdUsuario = reader.GetInt32(0);
                            UsuarioCache.LoginName = reader.GetString(1);
                            UsuarioCache.Password = reader.GetString(2);
                            UsuarioCache.Nombre = reader.GetString(3);
                            UsuarioCache.Apellido = reader.GetString(4);
                            UsuarioCache.Dni = reader.GetString(5);
                            UsuarioCache.Rol = reader.GetString(7);
                            UsuarioCache.Correo = reader.GetString(8);
                            UsuarioCache.Direccion = reader.GetString(9);
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
                    command.CommandText = "select Foto from USUARIOS where IdUsuario = @idUsuario";
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
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
