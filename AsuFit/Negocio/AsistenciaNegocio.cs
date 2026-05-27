using AsuFit.Datos;
using AsuFit.Entidades;

namespace AsuFit.Negocio
{
    // Procesa las reglas de negocio para el control de accesos y asistencias.
    public class AsistenciaNegocio
    {
        private AsistenciaDatos datos = new AsistenciaDatos();

        #region REGISTRO DE ASISTENCIA
        // Valida y delega la persistencia de una nueva marca de asistencia.
        public bool RegistrarAsistencia(Asistencia obj)
        {
            return datos.RegistrarAsistencia(obj);
        }
        #endregion
    }
}