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
                            tipo.Foto = (byte[])Resultado.GetValue(5);

                        }
                    }

                    Tabla.Load(Resultado);
                    Resultado.Close();
                    return tipo;

                }
            }
        }




            public void AgregarTipoEquipo(TipoEquipo tipoEquipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@tipo", tipoEquipo.Tipo);
                    command.Parameters.AddWithValue("@marca", tipoEquipo.Marca);
                    command.Parameters.AddWithValue("@modelo", tipoEquipo.Modelo);
                    command.Parameters.AddWithValue("@detalle", tipoEquipo.Detalle);
                    command.Parameters.AddWithValue("@foto", tipoEquipo.Foto);
                    command.CommandText = "sp_AgregarTipoEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public void EditarTipoEquipo(TipoEquipo tipoEquipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idTipoEquipo", tipoEquipo.IdTipoEquipo);
                    command.Parameters.AddWithValue("@tipo", tipoEquipo.Tipo);
                    command.Parameters.AddWithValue("@marca", tipoEquipo.Marca);
                    command.Parameters.AddWithValue("@modelo", tipoEquipo.Modelo);
                    command.Parameters.AddWithValue("@detalle", tipoEquipo.Detalle);
                    command.Parameters.AddWithValue("@foto", tipoEquipo.Foto);
                    command.CommandText = "sp_EditarTipoEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public void EliminarTipoEquipo(int idTipoEquipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idTipoEquipo", idTipoEquipo);
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
