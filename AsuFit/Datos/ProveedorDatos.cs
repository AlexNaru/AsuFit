using AsuFit.Entidades;
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

        // Unificamos Insertar y Editar en un solo método limpio
        public bool Guardar(Proveedor obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "";
                    if (obj.IdProveedor == 0) // Si es 0, es un proveedor nuevo
                    {
                        query = @"INSERT INTO Proveedores (Nombre, RUC, Categoria, Contacto, Telefono, Correo, Direccion, Ciudad, Estado) 
                                  VALUES (@Nombre, @RUC, @Categoria, @Contacto, @Telefono, @Correo, @Direccion, @Ciudad, @Estado)";
                    }
                    else // Si tiene ID, es una edición
                    {
                        query = @"UPDATE Proveedores SET 
                                  Nombre = @Nombre, RUC = @RUC, Categoria = @Categoria, Contacto = @Contacto, 
                                  Telefono = @Telefono, Correo = @Correo, Direccion = @Direccion, 
                                  Ciudad = @Ciudad, Estado = @Estado 
                                  WHERE IdProveedor = @IdProveedor";
                    }

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