using Pos.Domain.Model;
using Pos.Domain.Ports;

namespace Pos.Infrastructure.Repositories;

public class InMemoryEvaluationRepository : IEvaluationRepository
{
    private readonly List<Evaluation> _data = new();

    public void Add(Evaluation evaluation) => _data.Add(evaluation);

    public IReadOnlyList<Evaluation> ListByStudent(StudentId studentId)
    {
        return _data.Where(e => e.StudentId == studentId).ToList();
    }
}