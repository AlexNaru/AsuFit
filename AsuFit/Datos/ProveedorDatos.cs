using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class ProveedorDatos
    {
        public DataTable Listar()
        {
            DataTable tabla = new DataTable();
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT * FROM Proveedores ORDER BY Nombre ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    da.Fill(tabla);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la tabla Proveedores: " + ex.Message);
                }
            }
            return tabla;
        }

        public bool Insertar(string nombre, string ruc, string categoria, string contacto, string telefono, string correo, string direccion, string ciudad, string estado)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"INSERT INTO Proveedores (Nombre, RUC, Categoria, Contacto, Telefono, Correo, Direccion, Ciudad, Estado) 
                                     VALUES (@Nombre, @RUC, @Categoria, @Contacto, @Telefono, @Correo, @Direccion, @Ciudad, @Estado)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@RUC", ruc);
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    cmd.Parameters.AddWithValue("@Contacto", contacto);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", ciudad);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar proveedor: " + ex.Message);
                }
            }
        }

        public bool Editar(int idProveedor, string nombre, string ruc, string categoria, string contacto, string telefono, string correo, string direccion, string ciudad, string estado)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Proveedores SET 
                                     Nombre = @Nombre, RUC = @RUC, Categoria = @Categoria, Contacto = @Contacto, 
                                     Telefono = @Telefono, Correo = @Correo, Direccion = @Direccion, 
                                     Ciudad = @Ciudad, Estado = @Estado 
                                     WHERE IdProveedor = @IdProveedor";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@RUC", ruc);
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    cmd.Parameters.AddWithValue("@Contacto", contacto);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", ciudad);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar proveedor: " + ex.Message);
                }
            }
        }

        public bool CambiarEstado(int idProveedor, string nuevoEstado)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Proveedores SET Estado = @Estado WHERE IdProveedor = @IdProveedor";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);

                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al cambiar estado del proveedor: " + ex.Message);
                }
            }
        }
    }
}