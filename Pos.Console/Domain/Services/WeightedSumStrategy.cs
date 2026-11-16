using Pos.Domain.Model;

namespace Pos.Domain.Services;


public class WeightedSumStrategy : IFinalScoreStrategy
{
    public double Calculate(
        IReadOnlyList<Evaluation> evaluations,
        IReadOnlyDictionary<CriterionId, Criterion> criteria)
    {
        var scores = evaluations.ToDictionary(e => e.CriterionId, e => e.Score.Value);
        var weightSum = criteria.Values.Sum(c => c.Weight);
        if (weightSum <= 0)
            return 0;

        double total = 0;
        foreach (var (id, criterion) in criteria)
        {
            var score = scores.GetValueOrDefault(id, 0);
            total += (score / criterion.MaxScore) * criterion.Weight;
        }

        return Math.Round(total / weightSum * 100, 2);
    }
}