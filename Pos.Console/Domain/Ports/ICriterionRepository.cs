using Pos.Domain.Model;

namespace Pos.Domain.Ports;

public interface ICriterionRepository
{
    void Add(Criterion criterion);
    Criterion? Get(CriterionId id);
    IReadOnlyList<Criterion> ListAll();
}