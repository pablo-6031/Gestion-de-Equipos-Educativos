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

        public DataTable FiltrarUsuarios(string texto)
        {
            DataTable listaProveedores = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_FiltrarUsuarios", connection))
                {
                    command.Parameters.AddWithValue("@texto", texto);
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaProveedores.Load(reader);
                    reader.Close();
                }
            }
            return listaProveedores;
        }

        public string AgregarUsuario(Usuario usuario)
        {


            string resultado = string.Empty;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_AgregarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros
                        command.Parameters.AddWithValue("@loginName", usuario.LoginName);
                        command.Parameters.AddWithValue("@password", usuario.Password);
                        command.Parameters.AddWithValue("@nombres", usuario.Nombres);
                        command.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                        command.Parameters.AddWithValue("@rol", usuario.Rol);
                        command.Parameters.AddWithValue("@correo", usuario.Correo);
                        command.Parameters.AddWithValue("@cuil", usuario.Cuil);
                        command.Parameters.AddWithValue("@foto", usuario.Foto);
                        command.Parameters.AddWithValue("@id_institucion", usuario.IdInstitucion);

                        // Captura mensajes del procedimiento almacenado
                        connection.InfoMessage += (sender, e) =>
                        {
                            foreach (SqlError error in e.Errors)
                            {
                                resultado = error.Message; // Captura el mensaje del servidor
                            }
                        };

                        // Ejecuta el procedimiento
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    resultado = "Error SQL: " + ex.Message; // Retorna el mensaje de error
                }
                catch (Exception ex)
                {
                    resultado = "Error general: " + ex.Message; // Retorna otro tipo de error
                }
            }

            return resultado; // Retorna el mensaje capturado

        }

        public string EditarUsuario(Usuario usuario)
        {

            string resultado = string.Empty;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_EditarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);
                        command.Parameters.AddWithValue("@loginName", usuario.LoginName);
                        command.Parameters.AddWithValue("@password", usuario.Password);
                        command.Parameters.AddWithValue("@nombres", usuario.Nombres);
                        command.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                        command.Parameters.AddWithValue("@rol", usuario.Rol);
                        command.Parameters.AddWithValue("@correo", usuario.Correo);
                        command.Parameters.AddWithValue("@cuil", usuario.Cuil);
                        command.Parameters.AddWithValue("@foto", usuario.Foto);
                        command.Parameters.AddWithValue("@id_institucion", usuario.IdInstitucion);
                        // Captura mensajes del servidor (PRINT y RAISERROR)
                        connection.InfoMessage += (sender, e) =>
                        {
                            foreach (SqlError error in e.Errors)
                            {
                                resultado = error.Message; // Captura el mensaje
                            }
                        };

                        // Ejecuta el procedimiento almacenado
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    resultado = "Error SQL: " + ex.Message; // Retorna el mensaje del error
                }
                catch (Exception ex)
                {
                    resultado = "Error general: " + ex.Message; // Retorna otro tipo de error
                }
            }

            return resultado; // Retorna el mensaje capturado o el mensaje de error

        }


        public void EliminarUsuario(int IdUsuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id_usuario", IdUsuario);
                    command.CommandText = "sp_EliminarUsuario";
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
                            UsuarioCache.Apellido = reader.GetString(1);
                            UsuarioCache.Nombre = reader.GetString(2);
                            UsuarioCache.Cuil = reader.GetString(3);
                            UsuarioCache.Foto = !reader.IsDBNull(4) ? (byte[])reader.GetValue(4) : null;
                            UsuarioCache.Rol = reader.GetString(5);
                            UsuarioCache.Correo = reader.GetString(6);
                            UsuarioCache.LoginName = reader.GetString(7);
                            UsuarioCache.Password = reader.GetString(8);
                            UsuarioCache.IdInstitucion = reader.GetInt32(9);




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
