using Pos.Domain.Exceptions;
using Pos.Domain.Factories;
using Pos.Domain.Model;
using Pos.Domain.Services;
using Xunit;

namespace Pos.Tests;

public class DomainTests
{
    [Fact]
    public void Score_Cannot_Exceed_Criterion_Max()
    {
        var factory = new EvaluationFactory();
        var student = new Student(new StudentId("s1"), "Иван");
        var criterion = new Criterion(new CriterionId("c1"), "ДЗ", 10, 1.0);

        Assert.Throws<DomainException>(() =>
            factory.Create(new EvaluationId("e1"), student, criterion, 11));
    }

    [Fact]
    public void Score_Cannot_Be_Negative()
    {
        var factory = new EvaluationFactory();
        var student = new Student(new StudentId("s1"), "Иван");
        var criterion = new Criterion(new CriterionId("c1"), "ДЗ", 10, 1.0);

        Assert.Throws<DomainException>(() =>
            factory.Create(new EvaluationId("e1"), student, criterion, -1));
    }

    [Fact]
    public void FinalScore_Is_Weighted()
    {
        var factory = new EvaluationFactory();
        var student = new Student(new StudentId("s1"), "Иван");
        var c1 = new Criterion(new CriterionId("c1"), "Активность", 10, 0.4);
        var c2 = new Criterion(new CriterionId("c2"), "ДЗ", 10, 0.6);

        var e1 = factory.Create(new EvaluationId("e1"), student, c1, 8);
        var e2 = factory.Create(new EvaluationId("e2"), student, c2, 9);

        var calculator = new FinalScoreCalculator(new WeightedSumStrategy());
        var result = calculator.Calculate(new[] { e1, e2 }, new[] { c1, c2 });

        Assert.Equal(86.0, result);
    }
}