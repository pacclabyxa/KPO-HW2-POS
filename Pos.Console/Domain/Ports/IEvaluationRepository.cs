using Pos.Domain.Model;

namespace Pos.Domain.Ports;

public interface IEvaluationRepository
{
    void Add(Evaluation evaluation);
    IReadOnlyList<Evaluation> ListByStudent(StudentId studentId);
}