using Entities;
using System;
using System.Collections.Generic;
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
                    command.Parameters.AddWithValue("@Estado", acta.Estado);
                    command.Parameters.AddWithValue("@foto", acta.Foto);
                    command.Parameters.AddWithValue("@id_proveedor", acta.IdProveedor);
                    command.Parameters.AddWithValue("@id_institucion", acta.IdInstitucion);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para agregar una nueva acta
        public void AgregarActaConEquipos(Acta acta, List<Equipo> equipos)
        {
            decimal idActa ;

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
        private DataTable ConvertirListaADataTable(List<Equipo> equipos)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("IdEquipo", typeof(int));
            dataTable.Columns.Add("NumSerie", typeof(string));
            dataTable.Columns.Add("Matricula", typeof(string));
            dataTable.Columns.Add("Estado", typeof(string));
            dataTable.Columns.Add("Observacion", typeof(string));
            dataTable.Columns.Add("Destino", typeof(string));
            dataTable.Columns.Add("IdTipoEquipo", typeof(int));
            dataTable.Columns.Add("IdActa", typeof(int));


            foreach (var equipo in equipos)
            {
                dataTable.Rows.Add(equipo.IdEquipo, equipo.NumSerie, equipo.Matricula, equipo.Estado, equipo.Observacion, equipo.Destino, equipo.IdTipoEquipo, equipo.IdActa);
            }

            return dataTable;
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
                    command.Parameters.AddWithValue("@Estado", acta.Estado);
                    command.Parameters.AddWithValue("@foto", acta.Foto);
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
                            Estado = reader["estado"].ToString(),
                            Foto = reader["foto"] != DBNull.Value ? (byte[])reader["foto"] : null,
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
