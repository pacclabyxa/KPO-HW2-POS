using Pos.Domain.Model;

namespace Pos.Domain.Ports;

public interface IStudentRepository
{
    void Add(Student student);
    Student? Get(StudentId id);
    IReadOnlyList<Student> ListAll();
}