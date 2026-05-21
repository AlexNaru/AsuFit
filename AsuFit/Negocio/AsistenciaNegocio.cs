using AsuFit.Datos;
using AsuFit.Entidades;

namespace AsuFit.Negocio
{
    public class AsistenciaNegocio
    {
        private AsistenciaDatos datos = new AsistenciaDatos();
        public bool RegistrarAsistencia(Asistencia obj)
        {
            return datos.RegistrarAsistencia(obj);
        }
    }
}