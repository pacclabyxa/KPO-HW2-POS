# Домашнее задание №2: Модуль «ПОС» (C#)

## Архитектура
Выбрана **Ports & Adapters (Hexagonal)**. 
Зависимости направлены строго внутрь:
`UI (Console) -> Application -> Domain <- Infrastructure`
* **Domain** — ядро (сущности, VO, порты, доменные сервисы). Не знает ни о чем снаружи.
* **Application** — сценарии использования (Facade).
* **Infrastructure** — реализации репозиториев (In-Memory) и экспорт в CSV.
* **Composition Root** — файл `Program.cs` (сборка DI-контейнера).

## DDD (Domain-Driven Design)
* **Ubiquitous Language**: Студент (Student), Критерий (Criterion), Оценка (Evaluation), Итог (Final Score).
* **Entity**: `Student`, `Criterion`, `Evaluation` (имеют идентификатор).
* **Value Object**: `Score`, `StudentId`, `CriterionId`, `EvaluationId` (иммутабельны, сравниваются по значению).
* **Инварианты**: 
  * `Score` не может быть отрицательным.
  * `Evaluation` не может превышать `Criterion.MaxScore`.
  * Оценку можно ставить только по существующему критерию.
* **Repository (Port)**: Интерфейсы `IStudentRepository`, `ICriterionRepository`, `IEvaluationRepository` в слое Domain.
* **Domain Service**: `FinalScoreCalculator` — расчет итога не принадлежит одной сущности.

## Паттерны GoF (3 штуки, по одному из каждой группы)
1. **Порождающий (Factory)**: `EvaluationFactory`. Закрывает боль создания оценки с проверкой всех инвариантов (диапазон баллов, наличие критерия).
2. **Структурный (Facade)**: `PosFacade`. Единая точка входа для консольного интерфейса, скрывает сложность работы с репозиториями.
3. **Поведенческий (Strategy)**: `IFinalScoreStrategy` и `WeightedSumStrategy`. Позволяет легко менять формулу расчета итогового балла без изменения доменной модели.

## DI-контейнер
Используется `Microsoft.Extensions.DependencyInjection`. Все зависимости собираются в `Program.cs` (Composition Root).

## Роль ИИ
ИИ использовался как ассистент для генерации шаблонного кода и написания тестов. Архитектурные решения (выделение агрегатов, выбор паттернов) спроектированы и проверены мной.

## Как запустить
1. Сборка: `dotnet build`
2. Запуск: `dotnet run --project Pos.Console`
3. Тесты: `dotnet test`