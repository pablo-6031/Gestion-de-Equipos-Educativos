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

        public void AgregarEquipo(Equipo equipo)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@numSerie", equipo.NumSerie);
                    command.Parameters.AddWithValue("@tipo", equipo.Tipo);
                    command.Parameters.AddWithValue("@proveedor", equipo.Proveedor);
                    command.Parameters.AddWithValue("@detalle", equipo.Detalle);
                    command.Parameters.AddWithValue("@foto", equipo.Foto);
                    command.CommandText = "sp_AgregarEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

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
                    command.Parameters.AddWithValue("@numSerie", equipo.NumSerie);
                    command.Parameters.AddWithValue("@tipo", equipo.Tipo);
                    command.Parameters.AddWithValue("@proveedor", equipo.Proveedor);
                    command.Parameters.AddWithValue("@detalle", equipo.Detalle);
                    command.Parameters.AddWithValue("@foto", equipo.Foto);
                    command.CommandText = "sp_EditarEquipo";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }


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
