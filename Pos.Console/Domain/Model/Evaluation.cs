namespace Pos.Domain.Model;

public class Evaluation
{
    public EvaluationId Id { get; }
    public StudentId StudentId { get; }
    public CriterionId CriterionId { get; }
    public Score Score { get; }
    public string Comment { get; }
    public DateTime CreatedAt { get; }

    public Evaluation(
        EvaluationId id,
        StudentId studentId,
        CriterionId criterionId,
        Score score,
        string comment = "",
        DateTime? createdAt = null)
    {
        Id = id;
        StudentId = studentId;
        CriterionId = criterionId;
        Score = score;
        Comment = comment;
        CreatedAt = createdAt ?? DateTime.UtcNow;
    }
}