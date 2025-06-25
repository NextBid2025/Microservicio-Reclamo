using Reclamo.Domain.Exceptions;

namespace Reclamo.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa el identificador de un usuario.
    /// </summary>
    public class UsuarioId
    {
        /// <summary>
        /// Valor del identificador del usuario.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UsuarioId"/>.
        /// </summary>
        /// <param name="value">Identificador del usuario.</param>
        /// <exception cref="ValueObjectValidationException">
        /// Se lanza si el identificador es nulo o vacío.
        /// </exception>
        public UsuarioId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValueObjectValidationException("El ID del usuario no puede estar vacío.");

            Value = value;
        }

        /// <summary>
        /// Devuelve el valor del identificador como cadena.
        /// </summary>
        public override string ToString() => Value;
    }
}