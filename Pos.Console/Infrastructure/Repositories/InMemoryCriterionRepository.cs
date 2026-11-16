using Pos.Domain.Model;
using Pos.Domain.Ports;

namespace Pos.Infrastructure.Repositories;

public class InMemoryCriterionRepository : ICriterionRepository
{
    private readonly Dictionary<CriterionId, Criterion> _data = new();

    public void Add(Criterion criterion) => _data[criterion.Id] = criterion;
    public Criterion? Get(CriterionId id) => _data.GetValueOrDefault(id);
    public IReadOnlyList<Criterion> ListAll() => _data.Values.ToList();
}