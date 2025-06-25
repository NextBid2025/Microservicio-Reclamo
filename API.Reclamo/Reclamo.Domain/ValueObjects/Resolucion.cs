using Reclamo.Domain.Exceptions;

/// <summary>
/// Value Object que representa la resolución de un reclamo.
/// </summary>
public class Resolucion
{
    /// <summary>
    /// Valor de la resolución.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="Resolucion"/>.
    /// </summary>
    /// <param name="value">Valor de la resolución.</param>
    /// <exception cref="ValueObjectValidationException">
    /// Se lanza si el valor es nulo, vacío o supera los 200 caracteres.
    /// </exception>
    public Resolucion(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectValidationException("La resolución no puede estar vacía.");

        if (value.Length > 200)
            throw new ValueObjectValidationException("La resolución no puede tener más de 200 caracteres.");

        Value = value;
    }

    /// <summary>
    /// Devuelve el valor de la resolución como cadena.
    /// </summary>
    public override string ToString() => Value;
}