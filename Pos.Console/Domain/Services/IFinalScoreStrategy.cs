using Pos.Domain.Model;

namespace Pos.Domain.Services;

public interface IFinalScoreStrategy
{
    double Calculate(
        IReadOnlyList<Evaluation> evaluations,
        IReadOnlyDictionary<CriterionId, Criterion> criteria);
}