using AsuFit.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones de persistencia para la entidad Planes.
    public class PlanDatos
    {
        #region LECTURA DE DATOS
        // Recupera la lista de planes filtrada por estado.
        public List<Plan> ListarPlanes(string estado)
        {
            List<Plan> lista = new List<Plan>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
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
                catch (Exception) { throw; }
            }
            return lista;
        }

        // Busca y retorna un plan específico según su nombre.
        public Plan ObtenerPlanPorNombre(string nombrePlan)
        {
            Plan objPlan = null;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT IdPlan, NombrePlan, Precio, DuracionDias FROM Planes WHERE NombrePlan = @NombrePlan AND Estado = 'Activo'";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@NombrePlan", nombrePlan);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
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
            return objPlan;
        }
        #endregion

        #region ESCRITURA DE DATOS
        // Inserta un nuevo registro de plan en la base de datos.
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
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            return respuesta;
        }

        // Actualiza la información de un plan existente.
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
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            return respuesta;
        }

        // Modifica el estado lógico de un plan (Activo/Inactivo).
        public bool CambiarEstadoPlan(int idPlan, string nuevoEstado, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Planes SET Estado = @NuevoEstado WHERE IdPlan = @IdPlan";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdPlan", idPlan);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            return respuesta;
        }
        #endregion
    }
}