using Reclamo.Domain.Exceptions;

namespace Reclamo.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa el identificador de un reclamo.
    /// </summary>
    public class ReclamoId
    {
        /// <summary>
        /// Valor del identificador del reclamo.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ReclamoId"/>.
        /// </summary>
        /// <param name="value">Identificador del reclamo.</param>
        /// <exception cref="ValueObjectValidationException">
        /// Se lanza si el identificador es nulo o vacío.
        /// </exception>
        public ReclamoId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValueObjectValidationException("El ID del reclamo no puede estar vacío.");

            Value = value;
        }

        /// <summary>
        /// Devuelve el valor del identificador como cadena.
        /// </summary>
        public override string ToString() => Value;
    }
}