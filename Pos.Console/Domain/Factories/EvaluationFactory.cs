using Pos.Domain.Exceptions;
using Pos.Domain.Model;

namespace Pos.Domain.Factories;


public class EvaluationFactory
{
    public Evaluation Create(
        EvaluationId evaluationId,
        Student student,
        Criterion criterion,
        double scoreValue,
        string comment = "")
    {
        if (student is null)
            throw new DomainException("Нельзя выставить оценку несуществующему студенту");
        if (criterion is null)
            throw new DomainException("Нельзя оценить критерий, которого нет в рубрикаторе");
        if (scoreValue < 0 || scoreValue > criterion.MaxScore)
            throw new DomainException($"Балл {scoreValue} вне диапазона [0, {criterion.MaxScore}]");

        return new Evaluation(
            evaluationId,
            student.Id,
            criterion.Id,
            new Score(scoreValue),
            comment);
    }
}