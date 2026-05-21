using System;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            // Esta ruta universal (.\SQLEXPRESS) funcionará en tu PC y en la notebook de tu compañero
            string cadenaConexion = "Data Source=.\\SQLEXPRESS;Initial Catalog=AsuFitDB;Integrated Security=True;";
            return new SqlConnection(cadenaConexion);
        }
    }
}