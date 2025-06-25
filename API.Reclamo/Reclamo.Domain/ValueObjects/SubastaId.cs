using Reclamo.Domain.Exceptions;

namespace Reclamo.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa el identificador de una subasta.
    /// </summary>
    public class SubastaId
    {
        /// <summary>
        /// Valor del identificador de la subasta.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="SubastaId"/>.
        /// </summary>
        /// <param name="value">Identificador de la subasta.</param>
        /// <exception cref="ValueObjectValidationException">
        /// Se lanza si el identificador es nulo o vacío.
        /// </exception>
        public SubastaId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValueObjectValidationException("El ID de la subasta no puede estar vacío.");

            Value = value;
        }

        /// <summary>
        /// Devuelve el valor del identificador como cadena.
        /// </summary>
        public override string ToString() => Value;
    }
}