using Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
    public class AdMovilDao : ConnectionToSql
    {
        // Método para listar todos los anuncios móviles
        public DataTable ListarAdMoviles()
        {
            DataTable listaAds = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_ListarAdMoviles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaAds.Load(reader);
                    reader.Close();
                }
            }
            return listaAds;
        }

        // Método para agregar un nuevo anuncio móvil
        public void AgregarAdMovil(AdMovil adMovil)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_AgregarAdMovil", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@matricula", adMovil.Matricula);
                    command.Parameters.AddWithValue("@num_serie", adMovil.NumSerie);
                    command.Parameters.AddWithValue("@estado", adMovil.Estado);
                    command.Parameters.AddWithValue("@observacion", adMovil.Observacion);
                    command.Parameters.AddWithValue("@id_acta", adMovil.IdActa);
                    //command.Parameters.AddWithValue("@id_acta", adMovil.IdActa ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para editar un anuncio móvil existente
        public void EditarAdMovil(AdMovil adMovil)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EditarAdMovil", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_admovil", adMovil.IdAdMovil);
                    command.Parameters.AddWithValue("@matricula", adMovil.Matricula);
                    command.Parameters.AddWithValue("@num_serie", adMovil.NumSerie);
                    command.Parameters.AddWithValue("@estado", adMovil.Estado);
                    command.Parameters.AddWithValue("@observacion", adMovil.Observacion);
                    command.Parameters.AddWithValue("@id_acta", adMovil.IdActa);
                    //command.Parameters.AddWithValue("@id_acta", adMovil.IdActa ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar un anuncio móvil
        public void EliminarAdMovil(int idAdMovil)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EliminarAdMovil", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_admovil", idAdMovil);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener la foto de un anuncio móvil
        public byte[] ObtenerFoto(int idAdMovil)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("SELECT foto FROM ADMoviles WHERE id_admovil = @id_admovil", connection))
                {
                    command.Parameters.AddWithValue("@id_admovil", idAdMovil);
                    reader = command.ExecuteReader();
                    if (reader.Read() && reader["foto"] != DBNull.Value)
                    {
                        fotoDB = (byte[])reader["foto"];
                    }
                    reader.Close();
                }
            }
            return fotoDB;
        }
    }
}
