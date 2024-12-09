using Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
    public class AlumnoDao : ConnectionToSql
    {
        // Método para listar todos los alumnos
        public DataTable ListarAlumnos()
        {
            DataTable listaAlumnos = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_ListarAlumnos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaAlumnos.Load(reader);
                    reader.Close();
                }
            }
            return listaAlumnos;
        }

        public DataTable FiltrarAlumnos(string texto)
        {
            DataTable listaAlumnos = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_FiltrarAlumnos", connection))
                {
                    command.Parameters.AddWithValue("@texto", texto);
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaAlumnos.Load(reader);
                    reader.Close();
                }
            }
            return listaAlumnos;
        }

        // Método para agregar un nuevo alumno
        public void AgregarAlumno(Alumno alumno, int idEquipo)
        {



            using (var connection = GetConnection())
            {
                decimal idAlumno;

                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Crear la Acta
                        using (var command = new SqlCommand("sp_AgregarAlumno", connection, transaction))
                        {
                            command.Connection = connection;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@apellidos", alumno.Apellidos);
                            command.Parameters.AddWithValue("@nombres", alumno.Nombres);
                            command.Parameters.AddWithValue("@curso", alumno.Curso);
                            command.Parameters.AddWithValue("@cuil", alumno.Cuil);
                            command.Parameters.AddWithValue("@foto", alumno.Foto);
                            command.Parameters.AddWithValue("@telefono", alumno.Telefono);
                            command.Parameters.AddWithValue("@id_institucion", alumno.IdInstitucion);

                            // Ejecutar el comando y leer el resultado
                            idAlumno = (decimal)command.ExecuteScalar();

                        }

                        // Insertar los equipos relacionados
                        using (var command = new SqlCommand("sp_AgregarDetalleAlumnoEquipo", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@id_alumno", idAlumno);
                            command.Parameters.AddWithValue("@id_equipo", idEquipo);
                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
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















        // Método para editar un alumno existente
        public void EditarAlumno(Alumno alumno)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EditarAlumno", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_alumno", alumno.IdAlumno);
                    command.Parameters.AddWithValue("@apellidos", alumno.Apellidos);
                    command.Parameters.AddWithValue("@nombres", alumno.Nombres);
                    command.Parameters.AddWithValue("@curso", alumno.Curso);
                    command.Parameters.AddWithValue("@cuil", alumno.Cuil);
                    command.Parameters.AddWithValue("@foto", alumno.Foto);
                    command.Parameters.AddWithValue("@telefono", alumno.Telefono);
                    command.Parameters.AddWithValue("@id_institucion", alumno.IdInstitucion);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        // Método para eliminar un alumno
        public void EliminarAlumno(int idAlumno)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EliminarAlumno", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_alumno", idAlumno);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener la foto de un alumno
        public byte[] ObtenerFoto(int idAlumno)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("SELECT foto FROM Alumnos WHERE id_alumno = @id_alumno", connection))
                {
                    command.Parameters.AddWithValue("@id_alumno", idAlumno);
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
