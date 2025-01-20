using VsExample.Domain.Entities.Questions;

namespace VsExample.Application.Features.QuestionSets.Dto;

public class QuestionSetsQueryResult
{
    public IEnumerable<QuestionSet> QuestionSets { get; init; } = [];
    public int TotalCount { get; init; }
}