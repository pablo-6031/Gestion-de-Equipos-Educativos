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
    public class PrestamoDao : ConnectionToSql
    {
        // Método para listar todos los préstamos
        public DataTable ListarPrestamos()
        {
            DataTable listaPrestamos = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_ListarPrestamos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaPrestamos.Load(reader);
                    reader.Close();
                }
            }
            return listaPrestamos;
        }

        public DataTable FiltrarPrestamos(string texto)
        {
            DataTable listaPrestamos = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_FiltrarPrestamos", connection))
                {
                    command.Parameters.AddWithValue("@texto ", texto);
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaPrestamos.Load(reader);
                    reader.Close();
                }
            }
            return listaPrestamos;
        }

        // Método para agregar una nueva acta
        public void AgregarPrestamo(Prestamo prestamo, List<Equipo> equipos)
        {
            decimal idPrestamo;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Crear la Acta
                        using (var command = new SqlCommand("sp_AgregarPrestamo", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Nombre ", prestamo.Nombre);
                            command.Parameters.AddWithValue("@Apellido ", prestamo.Apellido);
                            command.Parameters.AddWithValue("@Dni ", prestamo.Dni);
                            command.Parameters.AddWithValue("@Funcion ", prestamo.Funcion);
                            command.Parameters.AddWithValue("@FechaPrestamo ", prestamo.FechaPrestamo);


                            // Ejecutar el comando y leer el resultado
                            idPrestamo = (decimal)command.ExecuteScalar();
                        }

                        // Insertar los equipos relacionados
                        using (var command = new SqlCommand("sp_AgregarEquipoPrestamo", connection, transaction))
                        {
                            foreach (var equipo in equipos)
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@id_prestamo", (int)idPrestamo);
                                command.Parameters.AddWithValue("@id_equipo", equipo.IdEquipo);

                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                            }


                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        

        // Método para editar un préstamo existente
        public void EditarPrestamo(Prestamo prestamo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EditarPrestamo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_prestamo", prestamo.IdPrestamo);
                    command.Parameters.AddWithValue("@nombre", prestamo.Nombre);
                    command.Parameters.AddWithValue("@apellido", prestamo.Apellido);
                    command.Parameters.AddWithValue("@dni", prestamo.Dni ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@funcion", prestamo.Funcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fecha_prestamo", prestamo.FechaPrestamo);
                    command.ExecuteNonQuery();
                }
            }
        }

       

        // Método para obtener un préstamo específico
        public Prestamo ObtenerPrestamo(int idPrestamo)
        {
            Prestamo prestamo = null;
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Prestamo WHERE id_prestamo = @id_prestamo", connection))
                {
                    command.Parameters.AddWithValue("@id_prestamo", idPrestamo);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        prestamo = new Prestamo
                        {
                            IdPrestamo = Convert.ToInt32(reader["id_prestamo"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = reader["dni"] != DBNull.Value ? reader["dni"].ToString() : null,
                            Funcion = reader["funcion"] != DBNull.Value ? reader["funcion"].ToString() : null,
                            FechaPrestamo = Convert.ToDateTime(reader["fecha_prestamo"])
                        };
                    }
                    reader.Close();
                }
            }
            return prestamo;
        }
    }
}
