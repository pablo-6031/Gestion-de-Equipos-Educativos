using Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
    public class ActaDao : ConnectionToSql
    {
        // Método para listar todas las actas
        public DataTable ListarActas()
        {
            DataTable listaActas = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_ListarActas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaActas.Load(reader);
                    reader.Close();
                }
            }
            return listaActas;
        }

        // Método para agregar una nueva acta
        public void AgregarActa(Acta acta)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_AgregarActa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@num_acta", acta.NumeroActa);
                    command.Parameters.AddWithValue("@fecha_entrega", acta.FechaEntrega);
                    command.Parameters.AddWithValue("@responsable", acta.Responsable);
                    command.Parameters.AddWithValue("@num_expediente", acta.NumeroExpediente);
                    command.Parameters.AddWithValue("@foto", acta.Foto);
                    command.Parameters.AddWithValue("@id_unidad", acta.IdUnidad);
                    command.Parameters.AddWithValue("@id_proveedor", acta.IdProveedor);
                    command.Parameters.AddWithValue("@id_institucion", acta.IdInstitucion);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para editar una acta existente
        public void EditarActa(Acta acta)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EditarActa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_acta", acta.IdActa);
                    command.Parameters.AddWithValue("@num_acta", acta.NumeroActa);
                    command.Parameters.AddWithValue("@fecha_entrega", acta.FechaEntrega);
                    command.Parameters.AddWithValue("@responsable", acta.Responsable);
                    command.Parameters.AddWithValue("@num_expediente", acta.NumeroExpediente);
                    command.Parameters.AddWithValue("@foto", acta.Foto);
                    command.Parameters.AddWithValue("@id_unidad", acta.IdUnidad);
                    command.Parameters.AddWithValue("@id_proveedor", acta.IdProveedor);
                    command.Parameters.AddWithValue("@id_institucion", acta.IdInstitucion);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar una acta
        public void EliminarActa(int idActa)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EliminarActa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_acta", idActa);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener los detalles de una acta específica
        public Acta ObtenerActa(int idActa)
        {
            Acta acta = null;
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_ObtenerActa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_acta", idActa);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        acta = new Acta
                        {
                            IdActa = Convert.ToInt32(reader["id_acta"]),
                            NumeroActa = reader["num_acta"].ToString(),
                            FechaEntrega = Convert.ToDateTime(reader["fecha_entrega"]),
                            Responsable = reader["responsable"].ToString(),
                            NumeroExpediente = reader["num_expediente"].ToString(),
                            Foto = reader["foto"] != DBNull.Value ? (byte[])reader["foto"] : null,
                            IdUnidad = Convert.ToInt32(reader["id_unidad"]),
                            IdProveedor = Convert.ToInt32(reader["id_proveedor"]),
                            IdInstitucion = Convert.ToInt32(reader["id_institucion"])
                        };
                    }
                    reader.Close();
                }
            }
            return acta;
        }
    }
}
