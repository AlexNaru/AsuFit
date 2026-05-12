using AsuFit.Datos;
using AsuFit.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace AsuFit.Negocio
{
    public class SocioNegocio
    {
        private SocioDatos objSocioDatos = new SocioDatos();

        public bool RegistrarSocio(Socio objSocio, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objSocio.Cedula))
            {
                mensaje = "El número de Cédula es obligatorio para registrar al socio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objSocio.Nombre) || string.IsNullOrWhiteSpace(objSocio.Apellido))
            {
                mensaje = "El nombre y el apellido no pueden estar vacíos.";
                return false;
            }

            if (objSocio.IdPlan <= 0)
            {
                mensaje = "Debe seleccionar un plan válido para el socio.";
                return false;
            }

            bool respuestaBD = objSocioDatos.RegistrarSocio(objSocio);

            if (respuestaBD == false)
            {
                mensaje = "Error al conectar o guardar en la base de datos SQL Server.";
            }

            return respuestaBD;
        }

        // NUEVO MÉTODO: Solo guarda al socio y nos devuelve su ID para usarlo en la Caja
        public int InsertarSocioYObtenerId(Socio nuevoSocio, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(nuevoSocio.Cedula) || string.IsNullOrWhiteSpace(nuevoSocio.Nombre))
            {
                mensaje = "La Cédula y el Nombre son obligatorios.";
                return 0;
            }

            return objSocioDatos.InsertarSocioYObtenerId(nuevoSocio, out mensaje);
        }

        public bool CambiarEstadoSocio(int idSocio, string nuevoEstado)
        {
            return objSocioDatos.CambiarEstadoSocio(idSocio, nuevoEstado);
        }

        public DataTable ListarSocios(string estado)
        {
            return objSocioDatos.ListarSocios(estado);
        }

        public bool EditarSocio(Socio obj)
        {
            return objSocioDatos.EditarSocio(obj);
        }

        public bool EliminarSocio(int idSocio)
        {
            return objSocioDatos.EliminarSocio(idSocio);
        }

        public bool ExisteCedula(string cedula, int idSocioActual)
        {
            return objSocioDatos.ExisteCedula(cedula, idSocioActual);
        }

        public Socio BuscarSocioPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula)) return null;
            return objSocioDatos.BuscarSocioPorCedula(cedula);
        }

        public Socio BuscarSocioPorId(int idSocio)
        {
            return objSocioDatos.BuscarSocioPorId(idSocio);
        }

        public void RegistrarAsistencia(int idSocio)
        {
            objSocioDatos.RegistrarAsistencia(idSocio);
        }

        public List<Socio> ListarVencidos()
        {
            SocioDatos datos = new SocioDatos();
            return datos.ListarVencidos();
        }

        public bool RenovarMembresiaSocio(int idSocio, int diasPlan)
        {
            return objSocioDatos.RenovarMembresiaSocio(idSocio, diasPlan);
        }
    }
}