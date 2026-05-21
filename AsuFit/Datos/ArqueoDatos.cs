using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class ArqueoDatos
    {
        // =======================================================
        // 1. ABRIR CAJA 
        // =======================================================
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

        // =======================================================
        // 2. VERIFICAR TURNO ABIERTO 
        // =======================================================
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

        // =======================================================
        // 3. CALCULAR TOTALES EN VIVO (MIGRADO A TABLA VENTAS)
        // =======================================================
        public DataTable ObtenerTotalesEnVivo(int idUsuario, DateTime fechaApertura)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                // Como eliminamos 'Pagos' y 'Gastos', la consulta es directa.
                // Todo el ingreso sale de 'Ventas' y el egreso de 'IngresosMercaderia'.
                string query = @"
                    DECLARE @Efectivo DECIMAL(18,2) = 0;
                    DECLARE @Transferencia DECIMAL(18,2) = 0;
                    DECLARE @Gastos DECIMAL(18,2) = 0;

                    -- 1. Ingresos en Efectivo (Cuotas y Productos unificados)
                    SET @Efectivo = ISNULL((SELECT SUM(Total) FROM Ventas WHERE MetodoPago = 'Efectivo' AND Fecha >= @FechaApertura), 0);

                    -- 2. Ingresos por Transferencia (Cuotas y Productos unificados)
                    SET @Transferencia = ISNULL((SELECT SUM(Total) FROM Ventas WHERE MetodoPago = 'Transferencia' AND Fecha >= @FechaApertura), 0);

                    -- 3. Egresos (Pago a proveedores de mercadería)
                    SET @Gastos = ISNULL((SELECT SUM(CostoTotal) FROM IngresosMercaderia WHERE FechaIngreso >= @FechaApertura), 0);

                    SELECT @Efectivo AS TotalEfectivo, @Transferencia AS TotalTransferencia, @Gastos AS TotalGastos;";

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@FechaApertura", fechaApertura);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // =======================================================
        // 4. HISTORIAL DE ARQUEOS 
        // =======================================================
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

        // =======================================================
        // 5. CERRAR TURNO 
        // =======================================================
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
    }
}