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
    public class EquipoDao : ConnectionToSql
    {
        public DataTable ListarEquipos()
        {
            DataTable listaEquipos = new DataTable();
            using (var connection = GetConnection())
            {
                System.Data.SqlClient.SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarEquipos";
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaEquipos.Load(reader);
                    reader.Close();
                    return listaEquipos;
                }
            }
        }


        public DataTable ListarEquiposPorActa(int idActa)
        {
            DataTable listaEquipos = new DataTable();

            using (var connection = GetConnection()) // Método para obtener la conexión
            {
                connection.Open();

                using (var command = new SqlCommand("sp_ListarEquiposPorActa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro del procedimiento almacenado
                    command.Parameters.AddWithValue("@id_acta", idActa);

                    // Ejecutar el comando y cargar los resultados en el DataTable
                    using (var reader = command.ExecuteReader())
                    {
                        listaEquipos.Load(reader);
                    }
                }
            }

            return listaEquipos;
        }


        public DataTable ListarEquiposporNumSerie(string valor)
        {
            DataTable listaEquipos = new DataTable();

            using (var connection = GetConnection()) // Método para obtener la conexión
            {
                connection.Open();

                using (var command = new SqlCommand("sp_ListarEquiposporNumSerie", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro del procedimiento almacenado
                    command.Parameters.AddWithValue("@var", valor);

                    // Ejecutar el comando y cargar los resultados en el DataTable
                    using (var reader = command.ExecuteReader())
                    {
                        listaEquipos.Load(reader);
                    }
                }
            }

            return listaEquipos;
        }

        public void AgregarEquipo(Equipo equipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@num_Serie", equipo.NumSerie);
                    command.Parameters.AddWithValue("@matricula", equipo.Matricula);
                    command.Parameters.AddWithValue("@estado", equipo.Estado);
                    command.Parameters.AddWithValue("@observacion", equipo.Observacion);
                    command.Parameters.AddWithValue("@destino", equipo.Destino);
                    command.Parameters.AddWithValue("@id_Tipo_Equipo", equipo.IdTipoEquipo);
                    command.Parameters.AddWithValue("@idActa", equipo.IdActa);
                    command.CommandText = "sp_AgregarEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }
            }
        }

        public void AgregarListaEquipo(List<Equipo> listEquipo, int idActa)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        // Insertar los equipos relacionados
                        using (var command = new SqlCommand("sp_AgregarEquipo", connection, transaction))
                        {
                            foreach (var equipo in listEquipo)
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@num_Serie", equipo.NumSerie);
                                command.Parameters.AddWithValue("@matricula", equipo.Matricula);
                                command.Parameters.AddWithValue("@estado", equipo.Estado);
                                command.Parameters.AddWithValue("@observacion", equipo.Observacion);
                                command.Parameters.AddWithValue("@destino", equipo.Destino);
                                command.Parameters.AddWithValue("@id_Tipo_Equipo", equipo.IdTipoEquipo);
                                command.Parameters.AddWithValue("@id_Acta",idActa);

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

        public void AgregarActaConEquipos(Acta acta, List<Equipo> equipos)
        {
            decimal idActa;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Crear la Acta
                        using (var command = new SqlCommand("sp_AgregarActa", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@num_acta", acta.NumeroActa);
                            command.Parameters.AddWithValue("@fecha_entrega", acta.FechaEntrega);
                            command.Parameters.AddWithValue("@responsable", acta.Responsable);
                            command.Parameters.AddWithValue("@Estado", acta.Estado);
                            command.Parameters.AddWithValue("@foto", acta.Foto);
                            command.Parameters.AddWithValue("@id_proveedor", acta.IdProveedor);
                            command.Parameters.AddWithValue("@id_institucion", acta.IdInstitucion);

                            // Ejecutar el comando y leer el resultado
                            idActa = (decimal)command.ExecuteScalar();
                        }

                        // Insertar los equipos relacionados
                        using (var command = new SqlCommand("sp_AgregarEquipo", connection, transaction))
                        {
                            foreach (var equipo in equipos)
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@num_Serie", equipo.NumSerie);
                                command.Parameters.AddWithValue("@matricula", equipo.Matricula);
                                command.Parameters.AddWithValue("@estado", equipo.Estado);
                                command.Parameters.AddWithValue("@observacion", equipo.Observacion);
                                command.Parameters.AddWithValue("@destino", equipo.Destino);
                                command.Parameters.AddWithValue("@id_Tipo_Equipo", equipo.IdTipoEquipo);
                                command.Parameters.AddWithValue("@id_Acta", (int)idActa);

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





        public void EditarEquipo(Equipo equipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idEquipo", equipo.IdEquipo);
                    command.Parameters.AddWithValue("@numSerie", equipo.NumSerie);
                    command.Parameters.AddWithValue("@matricula", equipo.Matricula);
                    command.Parameters.AddWithValue("@estado", equipo.Estado);
                    command.Parameters.AddWithValue("@observacion", equipo.Observacion);
                    command.Parameters.AddWithValue("@destino", equipo.Destino);
                    command.Parameters.AddWithValue("@idTipoEquipo", equipo.IdTipoEquipo);
                    command.Parameters.AddWithValue("@idActa", equipo.IdActa);
                    command.CommandText = "sp_EditarEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        /*
        public void EliminarEquipo(int IdEquipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idEquipo", IdEquipo);
                    command.CommandText = "sp_EliminarEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
        */
        public string EliminarEquipo(int idEquipo)
        {
            var mensajes = new List<string>(); // Lista para capturar los mensajes

            using (var connection = GetConnection())
            {
                connection.Open();

                // Capturar mensajes de PRINT en SQL Server
                connection.InfoMessage += (sender, e) =>
                {
                    foreach (SqlError error in e.Errors)
                    {
                        mensajes.Add(error.Message);
                    }
                };

                using (var command = new SqlCommand("sp_EliminarEquipo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro
                    command.Parameters.AddWithValue("@id_equipo", idEquipo);

                    // Ejecutar el procedimiento almacenado
                    command.ExecuteNonQuery();
                }
            }

            // Convertir la lista de mensajes en un único string
            return string.Join(Environment.NewLine, mensajes);
        }




        public byte[] ObtenerFoto(int idEquipo)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select Foto from EQUIPOS where IdEquipo = @idEquipo";
                    command.Parameters.AddWithValue("@idEquipo", idEquipo);
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
