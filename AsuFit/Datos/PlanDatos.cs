using AsuFit.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class PlanDatos
    {
        public List<Plan> ListarPlanes(string estado)
        {
            List<Plan> lista = new List<Plan>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Cambiamos el 'Activo' fijo por el parámetro @Estado
                    string query = "SELECT IdPlan, NombrePlan, Precio, DuracionDias FROM Planes WHERE Estado = @Estado";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Plan()
                            {
                                IdPlan = Convert.ToInt32(dr["IdPlan"]),
                                NombrePlan = dr["NombrePlan"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                DuracionDias = Convert.ToInt32(dr["DuracionDias"])
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

        public bool RegistrarPlan(Plan obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "INSERT INTO Planes (NombrePlan, Precio, DuracionDias) VALUES (@NombrePlan, @Precio, @DuracionDias)";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@NombrePlan", obj.NombrePlan);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@DuracionDias", obj.DuracionDias);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }

        public bool EditarPlan(Plan obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Planes SET NombrePlan = @NombrePlan, Precio = @Precio, DuracionDias = @DuracionDias WHERE IdPlan = @IdPlan";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@NombrePlan", obj.NombrePlan);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@DuracionDias", obj.DuracionDias);
                    cmd.Parameters.AddWithValue("@IdPlan", obj.IdPlan);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }

        public bool EliminarPlan(int idPlan, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Planes SET Estado = 'Inactivo' WHERE IdPlan = @IdPlan";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdPlan", idPlan);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }

        public Plan ObtenerPlanPorNombre(string nombrePlan)
        {
            Plan objPlan = null;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Buscamos el plan específico por su nombre
                    string query = "SELECT IdPlan, NombrePlan, Precio, DuracionDias FROM Planes WHERE NombrePlan = @NombrePlan AND Estado = 'Activo'";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@NombrePlan", nombrePlan);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Si encuentra el plan, armamos el objeto
                        {
                            objPlan = new Plan()
                            {
                                IdPlan = Convert.ToInt32(dr["IdPlan"]),
                                NombrePlan = dr["NombrePlan"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                DuracionDias = Convert.ToInt32(dr["DuracionDias"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error Técnico SQL al buscar plan: " + ex.Message);
                }
            }
            return objPlan; // Devuelve el plan con sus precios, o 'null' si no lo encontró
        }
    }
}