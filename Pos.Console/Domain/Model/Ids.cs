using Pos.Domain.Exceptions;

namespace Pos.Domain.Model;

public record StudentId
{
    public string Value { get; }
    public StudentId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("ID студента не может быть пустым");
        Value = value;
    }
}

public record CriterionId
{
    public string Value { get; }
    public CriterionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("ID критерия не может быть пустым");
        Value = value;
    }
}

public record EvaluationId
{
    public string Value { get; }
    public EvaluationId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("ID оценки не может быть пустым");
        Value = value;
    }
}