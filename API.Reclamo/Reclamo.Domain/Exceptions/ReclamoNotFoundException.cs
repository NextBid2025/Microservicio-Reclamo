using System;

namespace Reclamo.Domain.Exceptions
{
    /// <summary>
    /// Excepción que se lanza cuando no se encuentra un reclamo en el dominio.
    /// </summary>
    public class ReclamoNotFoundException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la excepción <see cref="ReclamoNotFoundException"/> con un mensaje específico.
        /// </summary>
        /// <param name="message">Mensaje que describe el error.</param>
        public ReclamoNotFoundException(string message) : base(message)
        {
        }
    }
}