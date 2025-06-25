using Reclamo.Domain.Exceptions;

namespace Reclamo.Domain.ValueObjects;

/// <summary>
/// Value Object que representa la descripción de un reclamo.
/// </summary>
public record Descripcion
{
    /// <summary>
    /// Valor de la descripción.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="Descripcion"/>.
    /// </summary>
    /// <param name="value">Valor de la descripción.</param>
    /// <exception cref="ValueObjectValidationException">
    /// Se lanza si la descripción es nula, vacía o excede los 500 caracteres.
    /// </exception>
    public Descripcion(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectValidationException("La descripción no puede estar vacía.");

        if (value.Length > 500)
            throw new ValueObjectValidationException("La descripción no puede exceder 500 caracteres.");

        Value = value;
    }

    /// <summary>
    /// Devuelve el valor de la descripción como cadena.
    /// </summary>
    public override string ToString() => Value;
}