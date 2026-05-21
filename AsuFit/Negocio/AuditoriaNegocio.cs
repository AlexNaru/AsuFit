using AsuFit.Datos;
using System.Data;

namespace AsuFit.Negocio
{
    public class AuditoriaNegocio
    {
        private AuditoriaDatos datos = new AuditoriaDatos();
        public DataTable ListarAuditoria()
        {
            return datos.ListarAuditoria();
        }
    }
}