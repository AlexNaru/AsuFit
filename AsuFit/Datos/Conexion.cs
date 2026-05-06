using System;
using System.Data.SqlClient;
using System.Configuration;

namespace AsuFit.Datos
{
    public class Conexion
    {
        // Este método nos devuelve la conexión lista para abrirse
        public static SqlConnection ObtenerConexion()
        {
            // Lee la cadena de conexión que guardamos en el archivo App.config
            string cadenaConexion = ConfigurationManager.ConnectionStrings["AsuFitConexion"].ConnectionString;
            return new SqlConnection(cadenaConexion);
        }
    }
}