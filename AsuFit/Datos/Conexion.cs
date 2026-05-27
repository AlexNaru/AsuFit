using System;
using System.Data.SqlClient;
using System.Configuration;

namespace AsuFit.Datos
{
    // Capa base de conectividad que proporciona instancias de conexión hacia el motor de base de datos.
    public class Conexion
    {
        #region CADENA DE CONEXIÓN
        // Genera y retorna un objeto de conexión inicializado mediante el ConfigurationManager.
        public static SqlConnection ObtenerConexion()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["AsuFitConexion"].ConnectionString;
            return new SqlConnection(cadenaConexion);
        }
        #endregion
    }
}