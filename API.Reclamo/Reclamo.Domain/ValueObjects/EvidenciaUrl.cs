using Reclamo.Domain.Exceptions;
using System;

namespace Reclamo.Domain.ValueObjects;

/// <summary>
/// Value Object que representa la URL de la evidencia de un reclamo.
/// </summary>
public record EvidenciaUrl
{
    /// <summary>
    /// Valor de la URL de la evidencia.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="EvidenciaUrl"/>.
    /// </summary>
    /// <param name="value">URL de la evidencia.</param>
    /// <exception cref="ValueObjectValidationException">
    /// Se lanza si la URL es nula, vacía, excede los 300 caracteres o no es una URL válida.
    /// </exception>
    public EvidenciaUrl(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectValidationException("La URL de la evidencia no puede estar vacía.");

        if (value.Length > 300)
            throw new ValueObjectValidationException("La URL de la evidencia no puede exceder 300 caracteres.");

        if (!Uri.TryCreate(value, UriKind.Absolute, out var uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            throw new ValueObjectValidationException("La URL de la evidencia no es válida.");

        Value = value;
    }

    /// <summary>
    /// Devuelve el valor de la URL como cadena.
    /// </summary>
    public override string ToString() => Value;
}