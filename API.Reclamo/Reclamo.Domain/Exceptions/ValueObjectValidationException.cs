namespace Reclamo.Domain.Exceptions;

/// <summary>
/// Excepción lanzada cuando un Value Object recibe un valor inválido.
/// </summary>
public class ValueObjectValidationException : Exception
{
    public ValueObjectValidationException(string mensaje) : base(mensaje) { }
}