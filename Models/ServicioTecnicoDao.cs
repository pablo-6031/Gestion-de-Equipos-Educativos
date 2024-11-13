using Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
    public class ServicioTecnicoDao : ConnectionToSql
    {
        // Método para listar todos los servicios técnicos
        public DataTable ListarServiciosTecnicos()
        {
            DataTable listaServicios = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_ListarServiciosTecnicos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaServicios.Load(reader);
                    reader.Close();
                }
            }
            return listaServicios;
        }

        // Método para agregar un nuevo servicio técnico
        public void AgregarServicioTecnico(ServicioTecnico servicio)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_AgregarServicioTecnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@falla", servicio.Falla);
                    command.Parameters.AddWithValue("@fecha_envio", servicio.FechaEnvio);
                    command.Parameters.AddWithValue("@foto", servicio.Foto);
                    command.Parameters.AddWithValue("@id_equipo", servicio.IdEquipo);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        // Método para editar un servicio técnico existente
        public void EditarServicioTecnico(ServicioTecnico servicio)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EditarServicioTecnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_servicio_tecnico", servicio.IdServicioTecnico);
                    command.Parameters.AddWithValue("@falla", servicio.Falla);
                    command.Parameters.AddWithValue("@fecha_envio", servicio.FechaEnvio);
                    command.Parameters.AddWithValue("@foto", servicio.Foto);
                    command.Parameters.AddWithValue("@id_equipo", servicio.IdEquipo);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        // Método para eliminar un servicio técnico
        public void EliminarServicioTecnico(int idServicioTecnico)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EliminarServicioTecnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_servicio_tecnico", idServicioTecnico);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener la foto de un servicio técnico
        public byte[] ObtenerFoto(int idServicioTecnico)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("SELECT foto FROM ServiciosTecnicos WHERE id_servicio_tecnico = @id_servicio_tecnico", connection))
                {
                    command.Parameters.AddWithValue("@id_servicio_tecnico", idServicioTecnico);
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
