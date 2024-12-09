using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Models
{
    public class TipoEquipoDao : ConnectionToSql
    {
        public DataTable ListarTipoEquipos()
        {
            DataTable listaTipoEquipos = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarTipoEquipos";
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaTipoEquipos.Load(reader);
                    reader.Close();
                    return listaTipoEquipos;
                }
            }
        }



        public DataTable FiltrarTipoEquipos(string texto)
        {
            DataTable listaProveedores = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_FiltrarTipoEquipos", connection))
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

        public List<string> ObtenerTiposEquiposUnicos()
        {
            List<string> tiposEquipos = new List<string>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_ObtenerTiposEquiposUnicos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tiposEquipos.Add(reader["tipo"].ToString());
                        }
                    }
                }
            }

            return tiposEquipos;
        }


        public TipoEquipo TraerTipoEquipos(int id)
        {
            DataTable Tabla = new DataTable();
            TipoEquipo tipo = new TipoEquipo();
            SqlDataReader Resultado;
            using (var connection = GetConnection())
            {
                SqlDataReader leerFilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id_tipo_equipo", id);
                    command.CommandText = "sp_TraerTipoEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    Resultado = command.ExecuteReader();
                    if (Resultado.HasRows)
                    {
                        while (Resultado.Read())
                        {
                            tipo.IdTipoEquipo = Resultado.GetInt32(0);
                            tipo.Tipo = Resultado.GetString(1);
                            tipo.Marca = Resultado.GetString(2);
                            tipo.Modelo = Resultado.GetString(3);
                            tipo.Detalle = Resultado.GetString(4);
                            tipo.Foto = !Resultado.IsDBNull(5) ? (byte[])Resultado.GetValue(5) : null;

                        }
                    }

                    Tabla.Load(Resultado);
                    Resultado.Close();
                    return tipo;

                }
            }
        }




            public string AgregarTipoEquipo(TipoEquipo tipoEquipo)
        {

            string resultado = string.Empty;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_AgregarTipoEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros
                        command.Parameters.AddWithValue("@tipo", tipoEquipo.Tipo);
                        command.Parameters.AddWithValue("@marca", tipoEquipo.Marca);
                        command.Parameters.AddWithValue("@modelo", tipoEquipo.Modelo);
                        command.Parameters.AddWithValue("@detalle_tecnico", tipoEquipo.Detalle);
                        command.Parameters.AddWithValue("@foto", tipoEquipo.Foto);

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

        public string EditarTipoEquipo(TipoEquipo tipoEquipo)
        {

            string resultado = string.Empty;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_EditarTipoEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros
                        command.Parameters.AddWithValue("@id_tipo_equipo", tipoEquipo.IdTipoEquipo);
                        command.Parameters.AddWithValue("@tipo", tipoEquipo.Tipo);
                        command.Parameters.AddWithValue("@marca", tipoEquipo.Marca);
                        command.Parameters.AddWithValue("@modelo", tipoEquipo.Modelo);
                        command.Parameters.AddWithValue("@detalle_tecnico", tipoEquipo.Detalle);
                        command.Parameters.AddWithValue("@foto", tipoEquipo.Foto);

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

        public void EliminarTipoEquipo(int idTipoEquipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id_tipo_equipo", idTipoEquipo);
                    command.CommandText = "sp_EliminarTipoEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }

        public byte[] ObtenerFoto(int idTipoEquipo)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Foto FROM Tipo_equipos WHERE id_tipo_equipo = @idTipoEquipo";
                    command.Parameters.AddWithValue("@idTipoEquipo", idTipoEquipo);
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        fotoDB = reader["Foto"] as byte[];
                    }
                    reader.Close();
                    return fotoDB;
                }
            }
        }
    }
}
