using Pos.Domain.Exceptions;

namespace Pos.Domain.Model;

public class Criterion
{
    public CriterionId Id { get; }
    public string Title { get; }
    public double MaxScore { get; }
    public double Weight { get; }

    public Criterion(CriterionId id, string title, double maxScore, double weight)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Название критерия не может быть пустым");
        if (maxScore <= 0)
            throw new DomainException("max_score должен быть > 0");
        if (weight <= 0)
            throw new DomainException("Вес критерия должен быть > 0");

        Id = id;
        Title = title;
        MaxScore = maxScore;
        Weight = weight;
    }
}