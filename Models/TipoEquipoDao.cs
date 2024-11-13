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
                    command.CommandText = "SELECT Foto FROM Tipo_equipos WHERE IdTipoEquipo = @idTipoEquipo";
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
