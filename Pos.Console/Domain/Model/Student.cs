using Pos.Domain.Exceptions;

namespace Pos.Domain.Model;

public class Student
{
    public StudentId Id { get; }
    public string Name { get; }

    public Student(StudentId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Имя студента не может быть пустым");
        Id = id;
        Name = name;
    }
}