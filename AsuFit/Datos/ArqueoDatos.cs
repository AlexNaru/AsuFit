using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones de apertura, monitoreo y cierre de turnos de caja (Arqueos).
    public class ArqueoDatos
    {
        #region GESTIÓN DE TURNOS
        // Inicia un nuevo turno de caja para un usuario específico.
        public bool AbrirCaja(TurnoCaja obj)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"INSERT INTO TurnosCaja (IdUsuario, CajeroNombre, FondoInicial, Estado) 
                                     VALUES (@idUser, @nombre, @fondo, 'Abierta')";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@idUser", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@nombre", obj.CajeroNombre);
                    cmd.Parameters.AddWithValue("@fondo", obj.FondoInicial);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
            return respuesta;
        }

        // Finaliza el turno de caja actual consolidando los montos contabilizados.
        public bool CerrarCaja(TurnoCaja obj)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE TurnosCaja SET 
                                     FechaCierre = GETDATE(),
                                     IngresosEfectivo = @ingEfvo,
                                     IngresosTransferencia = @ingTrans,
                                     GastosEfectivo = @gastos,
                                     MontoEsperado = @esperado,
                                     MontoContado = @contado,
                                     Diferencia = @diferencia,
                                     Estado = 'Cerrada'
                                     WHERE IdTurno = @idTurno";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@ingEfvo", obj.IngresosEfectivo);
                    cmd.Parameters.AddWithValue("@ingTrans", obj.IngresosTransferencia);
                    cmd.Parameters.AddWithValue("@gastos", obj.GastosEfectivo);
                    cmd.Parameters.AddWithValue("@esperado", obj.MontoEsperado);
                    cmd.Parameters.AddWithValue("@contado", obj.MontoContado);
                    cmd.Parameters.AddWithValue("@diferencia", obj.Diferencia);
                    cmd.Parameters.AddWithValue("@idTurno", obj.IdTurno);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
            return respuesta;
        }
        #endregion

        #region CONSULTAS Y REPORTES
        // Valida la existencia de un turno de caja activo para el usuario proporcionado.
        public DataTable ObtenerTurnoAbierto(int idUsuario)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                string query = "SELECT TOP 1 * FROM TurnosCaja WHERE IdUsuario = @idUser AND Estado = 'Abierta' ORDER BY IdTurno DESC";
                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@idUser", idUsuario);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Recupera las sumatorias dinámicas de ingresos y egresos vinculados a un turno activo.
        public DataTable ObtenerTotalesEnVivo(int idUsuario, DateTime fechaApertura)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                string query = @"
                    DECLARE @Efectivo DECIMAL(18,2) = 0;
                    DECLARE @Transferencia DECIMAL(18,2) = 0;
                    DECLARE @Gastos DECIMAL(18,2) = 0;

                    SET @Efectivo = ISNULL((SELECT SUM(Total) FROM Ventas WHERE MetodoPago = 'Efectivo' AND Fecha >= @FechaApertura), 0);
                    SET @Transferencia = ISNULL((SELECT SUM(Total) FROM Ventas WHERE MetodoPago = 'Transferencia' AND Fecha >= @FechaApertura), 0);
                    SET @Gastos = ISNULL((SELECT SUM(CostoTotal) FROM IngresosMercaderia WHERE FechaIngreso >= @FechaApertura), 0);

                    SELECT @Efectivo AS TotalEfectivo, @Transferencia AS TotalTransferencia, @Gastos AS TotalGastos;";

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@FechaApertura", fechaApertura);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Lista el historial de arqueos cerrados en un periodo determinado.
        public DataTable ListarHistorialArqueos(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT 
                                        IdTurno AS IdArqueo, 
                                        FechaApertura AS FechaHora, 
                                        MontoEsperado AS TotalIngresosSistema, 
                                        MontoContado AS EfectivoDeclarado, 
                                        Diferencia, 
                                        CajeroNombre AS UsuarioRegistra, 
                                        Estado 
                                     FROM TurnosCaja 
                                     WHERE CAST(FechaApertura AS DATE) >= @Desde 
                                     AND CAST(FechaApertura AS DATE) <= @Hasta
                                     ORDER BY FechaApertura DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return dt;
        }
        #endregion
    }
}