using System;
using System.Data.SqlClient;
using System.Configuration; // NUEVO: Necesario para leer el App.config / AsuFit.exe.config

namespace AsuFit.Datos
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            // Ahora va al archivo de configuración externo y busca la conexión llamada "AsuFitConexion"
            string cadenaConexion = ConfigurationManager.ConnectionStrings["AsuFitConexion"].ConnectionString;
            return new SqlConnection(cadenaConexion);
        }
    }
}