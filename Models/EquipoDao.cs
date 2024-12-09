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


        public DataTable ListarEquiposporNumSerieCompleto(string texto)
        {
            DataTable listaEquipos = new DataTable();
            using (var connection = GetConnection())
            {
                System.Data.SqlClient.SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@texto", texto);
                    command.CommandText = "sp_ListarEquiposporNumSerieCompleto";
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaEquipos.Load(reader);
                    reader.Close();
                    return listaEquipos;
                }
            }
        }




        public bool ComprobarEquipo(string numSerie, string matricula)
        {
            bool resultado = false;

            using (var connection = GetConnection()) // Método que obtiene la conexión a la base de datos
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_ComprobarEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros de entrada
                        command.Parameters.AddWithValue("@num_serie", numSerie);
                        command.Parameters.AddWithValue("@matricula", matricula);

                        // Agrega el parámetro de salida
                        SqlParameter resultadoParam = new SqlParameter("@resultado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultadoParam);

                        // Ejecuta el procedimiento almacenado
                        command.ExecuteNonQuery();

                        // Recupera el valor del parámetro de salida
                        resultado = (bool)resultadoParam.Value;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error SQL: " + ex.Message); // Loguear error de SQL
                    throw; // Puedes relanzar o manejar el error según sea necesario
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error general: " + ex.Message); // Loguear error general
                    throw;
                }
            }

            return resultado; // Retorna true si el equipo existe, false si no
        }




        public bool VerificarGarantiaEquipo(int idEquipo)
        {
            bool resultado = false;

            using (var connection = GetConnection()) // Método que obtiene la conexión a la base de datos
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_VerificarGarantiaEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros de entrada
                        command.Parameters.AddWithValue("@num_serie", idEquipo);

                        // Agrega el parámetro de salida
                        SqlParameter resultadoParam = new SqlParameter("@resultado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultadoParam);

                        // Ejecuta el procedimiento almacenado
                        command.ExecuteNonQuery();

                        // Recupera el valor del parámetro de salida
                        resultado = (bool)resultadoParam.Value;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error SQL: " + ex.Message); // Loguear error de SQL
                    throw; // Puedes relanzar o manejar el error según sea necesario
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error general: " + ex.Message); // Loguear error general
                    throw;
                }
            }

            return resultado; // Retorna true si el equipo existe, false si no
        }









        public DataTable ListarEquiposConTipo()
        {
            DataTable listaEquipos = new DataTable();
            using (var connection = GetConnection())
            {
                System.Data.SqlClient.SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarEquiposConTipo";
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaEquipos.Load(reader);
                    reader.Close();
                    return listaEquipos;
                }
            }
        }


        public DataTable ListarEquiposAdmPorActa(int idActa)
        {
            DataTable listaEquipos = new DataTable();

            using (var connection = GetConnection()) // Método para obtener la conexión
            {
                connection.Open();

                using (var command = new SqlCommand("sp_ListarEquiposAdmPorActa", connection))
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



        public DataTable ListarEquiposPorADMovil(int idAdm)
        {
            DataTable listaEquipos = new DataTable();

            using (var connection = GetConnection()) // Método para obtener la conexión
            {
                connection.Open();

                using (var command = new SqlCommand("sp_ListarEquiposPorADMovil", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro del procedimiento almacenado
                    command.Parameters.AddWithValue("@id_admovil", idAdm);

                    // Ejecutar el comando y cargar los resultados en el DataTable
                    using (var reader = command.ExecuteReader())
                    {
                        listaEquipos.Load(reader);
                    }
                }
            }

            return listaEquipos;
        }


        public DataTable ObtenerDetalleEquipo(int idEquipo)
        {
            DataTable equipo = new DataTable();

            using (var connection = GetConnection()) // Método para obtener la conexión
            {
                connection.Open();

                using (var command = new SqlCommand("sp_ObtenerDetalleEquipo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro del procedimiento almacenado
                    command.Parameters.AddWithValue("@IdEquipo", idEquipo);

                    // Ejecutar el comando y cargar los resultados en el DataTable
                    using (var reader = command.ExecuteReader())
                    {
                        equipo.Load(reader);
                    }
                }
            }

            return equipo;
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


        public void AgregarEquipo(Equipo equipo, int idAdm)
        {
            using (var connection = GetConnection())
            {
                decimal idEquipo;

                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Crear la Acta
                        using (var command = new SqlCommand("sp_AgregarEquipo", connection, transaction))
                        {
                            command.Connection = connection;
                            command.Parameters.AddWithValue("@num_Serie", equipo.NumSerie);
                            command.Parameters.AddWithValue("@matricula", equipo.Matricula);
                            command.Parameters.AddWithValue("@estado", equipo.Estado);
                            command.Parameters.AddWithValue("@observacion", equipo.Observacion);
                            command.Parameters.AddWithValue("@destino", equipo.Destino);
                            command.Parameters.AddWithValue("@id_Tipo_Equipo", equipo.IdTipoEquipo);
                            command.Parameters.AddWithValue("@Id_Acta", equipo.IdActa);
                            command.CommandType = CommandType.StoredProcedure;

                            // Ejecutar el comando y leer el resultado
                            idEquipo = (decimal)command.ExecuteScalar();

                        }

                        // Insertar los equipos relacionados
                        using (var command = new SqlCommand("sp_AgregarDetalleADMovilEquipo", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@id_admovil", idAdm);
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









        // Método para agregar una nueva acta
        public void AgregarActaConEquipos(Acta acta, List<Equipo> equipos, List<Equipo> adm)
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
                            command.Parameters.AddWithValue("@id_proveedor", acta.IdProveedor);
                            command.Parameters.AddWithValue("@id_institucion", acta.IdInstitucion);
                            if (acta.Foto != null && acta.Foto.Length > 0)
                                command.Parameters.Add("@foto", SqlDbType.VarBinary).Value = acta.Foto;
                            else
                                command.Parameters.Add("@foto", SqlDbType.VarBinary).Value = DBNull.Value;

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
                        // Insertar las actas relacionadas
                        using (var command = new SqlCommand("sp_AgregarAdMovil", connection, transaction))
                        {
                            foreach (var equipo in adm)
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@num_serie", equipo.NumSerie);
                                command.Parameters.AddWithValue("@matricula", equipo.Matricula);
                                command.Parameters.AddWithValue("@estado", equipo.Estado);
                                command.Parameters.AddWithValue("@observacion", equipo.Observacion);
                                command.Parameters.AddWithValue("@id_tipo_Equipo", equipo.IdTipoEquipo);
                                command.Parameters.AddWithValue("@id_acta", (int)idActa);


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





        public string EditarEquipo(Equipo equipo)
        {

            string resultado = string.Empty;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_EditarEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@Id_Equipo", equipo.IdEquipo);
                        command.Parameters.AddWithValue("@Num_Serie", equipo.NumSerie);
                        command.Parameters.AddWithValue("@Matricula", equipo.Matricula);
                        command.Parameters.AddWithValue("@Estado", equipo.Estado);
                        command.Parameters.AddWithValue("@Observacion", equipo.Observacion);
                        command.Parameters.AddWithValue("@Destino", equipo.Destino);
                        command.Parameters.AddWithValue("@Id_Tipo_Equipo", equipo.IdTipoEquipo);

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
