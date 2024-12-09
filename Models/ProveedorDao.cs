using Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
    public class ProveedorDao : ConnectionToSql
    {
        // Método para listar todos los proveedores
        public DataTable ListarProveedores()
        {
            DataTable listaProveedores = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_ListarProveedores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaProveedores.Load(reader);
                    reader.Close();
                }
            }
            return listaProveedores;
        }

        public DataTable FiltrarProveedores(string texto)
        {
            DataTable listaProveedores = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("sp_FiltrarProveedores", connection))
                {
                    command.Parameters.AddWithValue("@texto", texto);
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaProveedores.Load(reader);
                    reader.Close();
                }
            }
            return listaProveedores;
        }




        // Método para agregar un nuevo proveedor
        public string AgregarProveedor(Proveedor proveedor)
        {

            string resultado = string.Empty;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_AgregarProveedor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros
                        command.Parameters.AddWithValue("@nombre", proveedor.Nombre);
                        command.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                        command.Parameters.AddWithValue("@jurisdiccion", proveedor.Jurisdiccion);
                        command.Parameters.AddWithValue("@Correo", proveedor.Correo);
                        command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);

                        // Captura mensajes del procedimiento almacenado
                        connection.InfoMessage += (sender, e) =>
                        {
                            foreach (SqlError error in e.Errors)
                            {
                                resultado = error.Message; // Captura el mensaje del servidor
                            }
                        };

                        // Ejecuta el procedimiento
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    resultado = "Error SQL: " + ex.Message; // Retorna el mensaje de error
                }
                catch (Exception ex)
                {
                    resultado = "Error general: " + ex.Message; // Retorna otro tipo de error
                }
            }

            return resultado; // Retorna el mensaje capturado

        }

        // Método para editar un proveedor existente
        public string EditarProveedor(Proveedor proveedor)
        {


            string resultado = string.Empty;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_EditarProveedor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros
                        command.Parameters.AddWithValue("@id_proveedor", proveedor.IdProveedor);
                        command.Parameters.AddWithValue("@nombre", proveedor.Nombre);
                        command.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                        command.Parameters.AddWithValue("@jurisdiccion", proveedor.Jurisdiccion);
                        command.Parameters.AddWithValue("@Correo", proveedor.Correo);
                        command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);

                        // Captura mensajes del procedimiento almacenado
                        connection.InfoMessage += (sender, e) =>
                        {
                            foreach (SqlError error in e.Errors)
                            {
                                resultado = error.Message; // Captura el mensaje del servidor
                            }
                        };

                        // Ejecuta el procedimiento
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    resultado = "Error SQL: " + ex.Message; // Retorna el mensaje de error
                }
                catch (Exception ex)
                {
                    resultado = "Error general: " + ex.Message; // Retorna otro tipo de error
                }
            }

            return resultado; // Retorna el mensaje capturado

        }

        // Método para eliminar un proveedor
        public void EliminarProveedor(int idProveedor)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_EliminarProveedor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_proveedor", idProveedor);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener los detalles de un proveedor específico
        public Proveedor ObtenerProveedor(int idProveedor)
        {
            Proveedor proveedor = null;
            using (var connection = GetConnection())
            {
                SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Proveedor WHERE id_proveedor = @id_proveedor", connection))
                {
                    command.Parameters.AddWithValue("@id_proveedor", idProveedor);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        proveedor = new Proveedor
                        {
                            IdProveedor = Convert.ToInt32(reader["id_proveedor"]),
                            Nombre = reader["nombre"].ToString(),
                            Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : null,
                            Correo = reader["correo"] != DBNull.Value ? reader["correo"].ToString() : null,
                            Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : null
                        };
                    }
                    reader.Close();
                }
            }
            return proveedor;
        }
    }
}
