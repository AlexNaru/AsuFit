using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AsuFit.Entidades;

namespace AsuFit.Datos
{
    public class GastoDatos
    {
        public List<Gasto> ListarGastos()
        {
            List<Gasto> lista = new List<Gasto>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Traemos los gastos ordenados de más nuevo a más viejo
                    string query = "SELECT IdGasto, Descripcion, Categoria, Monto, FechaGasto, UsuarioRegistra FROM Gastos ORDER BY FechaGasto DESC";
                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Gasto()
                            {
                                IdGasto = Convert.ToInt32(dr["IdGasto"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                Monto = Convert.ToDecimal(dr["Monto"]),
                                FechaGasto = Convert.ToDateTime(dr["FechaGasto"]),
                                // Validamos por si el usuario es nulo en la base de datos
                                UsuarioRegistra = dr["UsuarioRegistra"] != DBNull.Value ? dr["UsuarioRegistra"].ToString() : ""
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return lista;
        }

        public bool RegistrarGasto(Gasto obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // La fecha se pone sola en SQL Server con GETDATE()
                    string query = "INSERT INTO Gastos (Descripcion, Categoria, Monto, UsuarioRegistra) VALUES (@desc, @cat, @monto, @user)";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@desc", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@cat", obj.Categoria);
                    cmd.Parameters.AddWithValue("@monto", obj.Monto);
                    cmd.Parameters.AddWithValue("@user", obj.UsuarioRegistra);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = "Error en Base de Datos: " + ex.Message;
                }
            }
            return respuesta;
        }
    }
}