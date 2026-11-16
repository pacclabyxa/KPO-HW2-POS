using Microsoft.Extensions.DependencyInjection;
using Pos.Application.Facade;
using Pos.Domain.Exceptions;
using Pos.Domain.Factories;
using Pos.Domain.Ports;
using Pos.Domain.Services;
using Pos.Infrastructure.Exporters;
using Pos.Infrastructure.Repositories;

// DI / Composition root
var services = new ServiceCollection();

services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();
services.AddSingleton<ICriterionRepository, InMemoryCriterionRepository>();
services.AddSingleton<IEvaluationRepository, InMemoryEvaluationRepository>();
services.AddSingleton<IFinalScoreStrategy, WeightedSumStrategy>();
services.AddSingleton<FinalScoreCalculator>();
services.AddSingleton<EvaluationFactory>();
services.AddSingleton<PosFacade>();
services.AddSingleton<CsvFinalScoreExporter>();

var provider = services.BuildServiceProvider();
var facade = provider.GetRequiredService<PosFacade>();
var exporter = provider.GetRequiredService<CsvFinalScoreExporter>();

Console.WriteLine("    ДЕМО-СЦЕНАРИЙ    ");
try
{
    facade.AddStudent("s1", "Иван Иванов");
    facade.AddCriterion("c1", "Активность", 10, 0.4);
    facade.AddCriterion("c2", "ДЗ", 10, 0.6);

    facade.Evaluate("e1", "s1", "c1", 8, "хорошо");
    facade.Evaluate("e2", "s1", "c2", 9, "отлично");

    var final = facade.FinalScore("s1");
    Console.WriteLine($"Итог: {final}");
    Console.WriteLine("CSV:");
    Console.WriteLine(exporter.Export("Иван Иванов", final));
}
catch (DomainException ex)
{
    Console.WriteLine("Ошибка демо: " + ex.Message);
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("1. Добавить студента");
    Console.WriteLine("2. Добавить критерий");
    Console.WriteLine("3. Показать студентов");
    Console.WriteLine("4. Показать критерии");
    Console.WriteLine("5. Выставить оценку");
    Console.WriteLine("6. Показать оценки студента");
    Console.WriteLine("7. Посчитать итог");
    Console.WriteLine("8. Экспорт итога в CSV");
    Console.WriteLine("0. Выход");
    Console.Write("> ");

    var choice = Console.ReadLine();
    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("ID студента: ");
                var sid = Console.ReadLine()!;
                Console.Write("Имя: ");
                var name = Console.ReadLine()!;
                facade.AddStudent(sid, name);
                Console.WriteLine("Студент добавлен.");
                break;

            case "2":
                Console.Write("ID критерия: ");
                var cid = Console.ReadLine()!;
                Console.Write("Название: ");
                var title = Console.ReadLine()!;
                Console.Write("Max score: ");
                var max = double.Parse(Console.ReadLine()!);
                Console.Write("Вес: ");
                var weight = double.Parse(Console.ReadLine()!);
                facade.AddCriterion(cid, title, max, weight);
                Console.WriteLine("Критерий добавлен.");
                break;

            case "3":
                foreach (var s in facade.ListStudents())
                    Console.WriteLine($"{s.Id.Value}: {s.Name}");
                break;

            case "4":
                foreach (var c in facade.ListCriteria())
                    Console.WriteLine($"{c.Id.Value}: {c.Title}, max={c.MaxScore}, weight={c.Weight}");
                break;

            case "5":
                Console.Write("ID оценки: ");
                var eid = Console.ReadLine()!;
                Console.Write("ID студента: ");
                var studentId = Console.ReadLine()!;
                Console.Write("ID критерия: ");
                var criterionId = Console.ReadLine()!;
                Console.Write("Балл: ");
                var score = double.Parse(Console.ReadLine()!);
                Console.Write("Комментарий (можно пусто): ");
                var comment = Console.ReadLine() ?? "";
                facade.Evaluate(eid, studentId, criterionId, score, comment);
                Console.WriteLine("Оценка выставлена.");
                break;

            case "6":
                Console.Write("ID студента: ");
                var listStudentId = Console.ReadLine()!;
                foreach (var e in facade.ListEvaluations(listStudentId))
                    Console.WriteLine($"{e.CriterionId.Value}: {e.Score.Value} ({e.Comment})");
                break;

            case "7":
                Console.Write("ID студента: ");
                var finalStudentId = Console.ReadLine()!;
                Console.WriteLine($"Итог: {facade.FinalScore(finalStudentId)}");
                break;

            case "8":
                Console.Write("ID студента: ");
                var expStudentId = Console.ReadLine()!;
                var student = facade.ListStudents().FirstOrDefault(s => s.Id.Value == expStudentId);
                if (student is null)
                {
                    Console.WriteLine("Студент не найден.");
                    break;
                }
                var fs = facade.FinalScore(expStudentId);
                Console.WriteLine(exporter.Export(student.Name, fs));
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Неизвестная команда.");
                break;
        }
    }
    catch (DomainException ex)
    {
        Console.WriteLine("Доменная ошибка: " + ex.Message);
    }
    catch (FormatException)
    {
        Console.WriteLine("Некорректный формат числа.");
    }
}