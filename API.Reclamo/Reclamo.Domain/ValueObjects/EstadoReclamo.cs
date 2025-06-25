using Reclamo.Domain.Exceptions;

/// <summary>
/// Value Object que representa el estado de un reclamo.
/// </summary>
public class EstadoReclamo
{
    /// <summary>
    /// Valor del estado.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="EstadoReclamo"/>.
    /// </summary>
    /// <param name="value">Valor del estado.</param>
    /// <exception cref="ValueObjectValidationException">
    /// Se lanza si el valor es nulo, vacío o supera los 20 caracteres.
    /// </exception>
    public EstadoReclamo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectValidationException("El estado no puede estar vacío.");

        if (value.Length > 20)
            throw new ValueObjectValidationException("El estado no puede tener más de 20 caracteres.");

        Value = value;
    }

    /// <summary>
    /// Devuelve el valor del estado como cadena.
    /// </summary>
    public override string ToString() => Value;
}