using AsuFit.Datos;     // Necesitamos hablar con la capa de Datos
using AsuFit.Entidades; // Necesitamos el objeto Socio
using System;
using System.Collections.Generic;
using System.Data;

namespace AsuFit.Negocio
{
    public class SocioNegocio
    {
        // Instanciamos la clase de Datos para poder enviarle la información
        private SocioDatos objSocioDatos = new SocioDatos();

        // Método que valida las reglas antes de guardar. 
        // Usamos 'out string mensaje' para devolverle un texto de error al formulario si algo falla.
        public bool RegistrarSocio(Socio objSocio, out string mensaje)
        {
            mensaje = string.Empty;

            // --- 1. REGLAS DE NEGOCIO Y VALIDACIONES ---

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

            // Aquí a futuro podemos agregar la validación de edad usando FechaNacimiento
            // Ej: Si es menor de 15 años, pedir un campo de "Autorización de Tutor".

            // --- 2. SI PASA LOS FILTROS, ENVIAMOS A LA BASE DE DATOS ---
            bool respuestaBD = objSocioDatos.RegistrarSocio(objSocio);

            if (respuestaBD == false)
            {
                mensaje = "Error al conectar o guardar en la base de datos SQL Server.";
            }

            return respuestaBD;
        }

        public bool RegistrarSocioYPrimerPago(Socio nuevoSocio, Pago pagoInicial, int diasPlan, out string mensaje)
        {
            mensaje = string.Empty;

            // 1. Validamos las mismas reglas de siempre para el socio
            if (string.IsNullOrWhiteSpace(nuevoSocio.Cedula) || string.IsNullOrWhiteSpace(nuevoSocio.Nombre))
            {
                mensaje = "La Cédula y el Nombre son obligatorios.";
                return false;
            }

            // 2. Llamamos a Datos para insertar el socio y que nos devuelva el ID numérico
            int nuevoIdSocio = objSocioDatos.InsertarSocioYObtenerId(nuevoSocio, out mensaje);

            if (nuevoIdSocio > 0)
            {
                // 3. ¡Éxito! Le asignamos ese nuevo ID al recibo de pago
                pagoInicial.IdSocio = nuevoIdSocio;

                // 4. Instanciamos PagoDatos y registramos el ingreso
                PagoDatos objPagoDatos = new PagoDatos();

                // ¡EL CAMBIO ESTÁ ACÁ! Pasamos un 0 en vez de diasPlan
                return objPagoDatos.RegistrarCobro(pagoInicial, 0, out mensaje);
            }
            else
            {
                // Si el ID volvió como 0, significa que falló el registro del socio
                return false;
            }
        }

        public bool CambiarEstadoSocio(int idSocio, string nuevoEstado)
        {
            // Solo le agregamos "Socio" en el medio para que use tu variable
            return objSocioDatos.CambiarEstadoSocio(idSocio, nuevoEstado);
        }

        public DataTable ListarSocios(string estado) // ¡Acá está el secreto! Le decimos que reciba la variable
        {
            // Ahora sí conoce qué es "estado" y se lo pasa a la capa de datos
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
            // Negocio no hace la conexión a la base de datos, 
            // solo le pasa la tarea a la capa de Datos.
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