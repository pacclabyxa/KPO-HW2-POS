using Pos.Domain.Exceptions;

namespace Pos.Domain.Model;

public record Score
{
    public double Value { get; }

    public Score(double value)
    {
        if (value < 0)
            throw new DomainException("Балл не может быть отрицательным");
        Value = value;
    }
}