using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Models
{
    public class ConnectionToSql
    {
        private readonly string connectionString;
        public ConnectionToSql()
        {
            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= C:\\Users\\pablo\\source\\repos\\Gestion de Equipos Educativos\\DataBase\\DB_GestionEquipos.mdf;Integrated Security=True;Connect Timeout=30";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }
}
