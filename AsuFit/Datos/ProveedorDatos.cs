using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones de persistencia para la entidad Proveedores.
    public class ProveedorDatos
    {
        #region OPERACIONES DE DATOS
        // Recupera la lista de proveedores ordenados alfabéticamente.
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

        // Realiza inserción o actualización de un registro de proveedor.
        public bool Guardar(Proveedor obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = (obj.IdProveedor == 0)
                        ? @"INSERT INTO Proveedores (Nombre, RUC, Categoria, Contacto, Telefono, Correo, Direccion, Ciudad, Estado) 
                            VALUES (@Nombre, @RUC, @Categoria, @Contacto, @Telefono, @Correo, @Direccion, @Ciudad, @Estado)"
                        : @"UPDATE Proveedores SET Nombre = @Nombre, RUC = @RUC, Categoria = @Categoria, Contacto = @Contacto, 
                            Telefono = @Telefono, Correo = @Correo, Direccion = @Direccion, Ciudad = @Ciudad, Estado = @Estado 
                            WHERE IdProveedor = @IdProveedor";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    if (obj.IdProveedor > 0) cmd.Parameters.AddWithValue("@IdProveedor", obj.IdProveedor);

                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@RUC", obj.RUC);
                    cmd.Parameters.AddWithValue("@Categoria", obj.Categoria);
                    cmd.Parameters.AddWithValue("@Contacto", obj.Contacto);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", obj.Ciudad);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al guardar proveedor: " + ex.Message);
                }
            }
        }

        // Modifica el estado lógico de un proveedor en el sistema.
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
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al cambiar estado del proveedor: " + ex.Message);
                }
            }
        }
        #endregion
    }
}