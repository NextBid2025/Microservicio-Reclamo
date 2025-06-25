using Reclamo.Domain.Exceptions;
namespace Reclamo.Domain.ValueObjects;

public class EstadoReclamo
{
    public string Value { get; private set; }

    public static readonly EstadoReclamo Pendiente = new EstadoReclamo("Pendiente");
    public static readonly EstadoReclamo EnRevision = new EstadoReclamo("EnRevision");
    public static readonly EstadoReclamo Resuelto = new EstadoReclamo("Resuelto");
    public static readonly EstadoReclamo Rechazado = new EstadoReclamo("Rechazado");

    public EstadoReclamo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectValidationException("El estado no puede estar vacío.");

        if (value.Length > 20)
            throw new ValueObjectValidationException("El estado no puede tener más de 20 caracteres.");

        Value = value;
    }

    public override string ToString() => Value;
}