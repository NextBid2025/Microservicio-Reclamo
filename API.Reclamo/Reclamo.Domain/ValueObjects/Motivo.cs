using Reclamo.Domain.Exceptions;

namespace Reclamo.Domain.ValueObjects;

/// <summary>
/// Value Object que representa el motivo de un reclamo.
/// </summary>
public record Motivo
{
    /// <summary>
    /// Valor del motivo.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="Motivo"/>.
    /// </summary>
    /// <param name="value">Valor del motivo.</param>
    /// <exception cref="ValueObjectValidationException">
    /// Se lanza si el motivo es nulo, vacío o excede los 100 caracteres.
    /// </exception>
    public Motivo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectValidationException("El motivo no puede estar vacío.");

        if (value.Length > 100)
            throw new ValueObjectValidationException("El motivo no puede exceder 100 caracteres.");

        Value = value;
    }

    /// <summary>
    /// Devuelve el valor del motivo como cadena.
    /// </summary>
    public override string ToString() => Value;
}