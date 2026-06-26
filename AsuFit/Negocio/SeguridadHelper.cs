using System;
using System.Text;
using System.Security.Cryptography;

namespace AsuFit.Negocio
{
    public static class SeguridadHelper
    {
        // Este método recibe texto plano (ej: "12345") y devuelve un hash indescifrable
        public static string HashearContrasena(string contrasenaPlana)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertimos el texto a bytes y calculamos el Hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contrasenaPlana));

                // Convertimos los bytes de nuevo a una cadena de texto hexadecimal
                StringBuilder constructor = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    constructor.Append(bytes[i].ToString("x2"));
                }
                return constructor.ToString();
            }
        }
    }
}