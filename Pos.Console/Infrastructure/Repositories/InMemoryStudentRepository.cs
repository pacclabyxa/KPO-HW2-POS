using Pos.Domain.Model;
using Pos.Domain.Ports;

namespace Pos.Infrastructure.Repositories;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly Dictionary<StudentId, Student> _data = new();

    public void Add(Student student) => _data[student.Id] = student;
    public Student? Get(StudentId id) => _data.GetValueOrDefault(id);
    public IReadOnlyList<Student> ListAll() => _data.Values.ToList();
}