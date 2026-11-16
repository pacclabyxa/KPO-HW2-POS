using Pos.Domain.Exceptions;
using Pos.Domain.Factories;
using Pos.Domain.Model;
using Pos.Domain.Ports;
using Pos.Domain.Services;

namespace Pos.Application.Facade;

public class PosFacade
{
    private readonly IStudentRepository _students;
    private readonly ICriterionRepository _criteria;
    private readonly IEvaluationRepository _evaluations;
    private readonly EvaluationFactory _evaluationFactory;
    private readonly FinalScoreCalculator _finalScoreCalculator;

    public PosFacade(
        IStudentRepository students,
        ICriterionRepository criteria,
        IEvaluationRepository evaluations,
        EvaluationFactory evaluationFactory,
        FinalScoreCalculator finalScoreCalculator)
    {
        _students = students;
        _criteria = criteria;
        _evaluations = evaluations;
        _evaluationFactory = evaluationFactory;
        _finalScoreCalculator = finalScoreCalculator;
    }

    public Student AddStudent(string id, string name)
    {
        var student = new Student(new StudentId(id), name);
        _students.Add(student);
        return student;
    }

    public Criterion AddCriterion(string id, string title, double maxScore, double weight)
    {
        var criterion = new Criterion(new CriterionId(id), title, maxScore, weight);
        _criteria.Add(criterion);
        return criterion;
    }

    public IReadOnlyList<Student> ListStudents() => _students.ListAll();
    public IReadOnlyList<Criterion> ListCriteria() => _criteria.ListAll();

    public Evaluation Evaluate(
        string evaluationId,
        string studentId,
        string criterionId,
        double score,
        string comment = "")
    {
        var student = _students.Get(new StudentId(studentId))
            ?? throw new DomainException("Студент не найден");

        var criterion = _criteria.Get(new CriterionId(criterionId))
            ?? throw new DomainException("Критерий не найден в рубрикаторе");

        var evaluation = _evaluationFactory.Create(
            new EvaluationId(evaluationId),
            student,
            criterion,
            score,
            comment);

        _evaluations.Add(evaluation);
        return evaluation;
    }

    public IReadOnlyList<Evaluation> ListEvaluations(string studentId)
    {
        return _evaluations.ListByStudent(new StudentId(studentId));
    }

    public double FinalScore(string studentId)
    {
        var student = _students.Get(new StudentId(studentId))
            ?? throw new DomainException("Студент не найден");

        var evaluations = _evaluations.ListByStudent(new StudentId(studentId));
        var criteria = _criteria.ListAll();

        return _finalScoreCalculator.Calculate(evaluations, criteria);
    }
}