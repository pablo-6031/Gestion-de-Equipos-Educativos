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
    public class InstitucionDao :  ConnectionToSql
    {
        public DataTable ListarInstituciones()
        {
            DataTable listaInstituciones = new DataTable();
            using (var connection = GetConnection())
            {
                System.Data.SqlClient.SqlDataReader reader;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarInstituciones";
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();
                    listaInstituciones.Load(reader);
                    reader.Close();
                    return listaInstituciones;
                }
            }
        }

        public void AgregarInstitucion(Institucion institucion)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_AgregarInstitucion";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", institucion.Nombre);
                    command.Parameters.AddWithValue("@cue", institucion.Cue);
                    command.Parameters.AddWithValue("@turno", institucion.Turno);
                    command.Parameters.AddWithValue("@director", institucion.Director);
                    command.Parameters.AddWithValue("@nivel", institucion.Nivel);
                    command.Parameters.AddWithValue("@calle", institucion.Calle);
                    command.Parameters.AddWithValue("@numeroCalle", institucion.NumeroCalle);
                    command.Parameters.AddWithValue("@barrio", institucion.Barrio);
                    command.Parameters.AddWithValue("@localidad", institucion.Localidad);
                    command.Parameters.AddWithValue("@provincia", institucion.Provincia);
                    command.Parameters.AddWithValue("@codigoPostal", institucion.CodigoPostal);
                    command.Parameters.AddWithValue("@region", institucion.region);
                    command.Parameters.AddWithValue("@telefono", institucion.Telefono);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public void EditarInstitucion(Institucion institucion)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_EditarInstitucion";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idInstitucion", institucion.IdInstitucion);
                    command.Parameters.AddWithValue("@nombre", institucion.Nombre);
                    command.Parameters.AddWithValue("@cue", institucion.Cue);
                    command.Parameters.AddWithValue("@turno", institucion.Turno);
                    command.Parameters.AddWithValue("@director", institucion.Director);
                    command.Parameters.AddWithValue("@nivel", institucion.Nivel);
                    command.Parameters.AddWithValue("@calle", institucion.Calle);
                    command.Parameters.AddWithValue("@numeroCalle", institucion.NumeroCalle);
                    command.Parameters.AddWithValue("@barrio", institucion.Barrio);
                    command.Parameters.AddWithValue("@localidad", institucion.Localidad);
                    command.Parameters.AddWithValue("@provincia", institucion.Provincia);
                    command.Parameters.AddWithValue("@codigoPostal", institucion.CodigoPostal);
                    command.Parameters.AddWithValue("@region", institucion.region);
                    command.Parameters.AddWithValue("@telefono", institucion.Telefono);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public void EliminarInstitucion(int idInstitucion)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_EliminarInstitucion";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idInstitucion", idInstitucion);
                    command.ExecuteNonQuery();
                }
            }
        }

        

    }
}
