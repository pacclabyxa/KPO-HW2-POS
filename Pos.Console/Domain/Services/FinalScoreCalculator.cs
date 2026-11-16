using Pos.Domain.Model;

namespace Pos.Domain.Services;

public class FinalScoreCalculator
{
    private readonly IFinalScoreStrategy _strategy;

    public FinalScoreCalculator(IFinalScoreStrategy strategy)
    {
        _strategy = strategy;
    }

    public double Calculate(
        IReadOnlyList<Evaluation> evaluations,
        IReadOnlyList<Criterion> criteria)
    {
        var criteriaMap = criteria.ToDictionary(c => c.Id, c => c);
        return _strategy.Calculate(evaluations, criteriaMap);
    }
}