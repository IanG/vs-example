using FluentResults;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Domain.Entities.Questions;

namespace VsExample.Application.Features.QuestionSets.Queries;

public class GetQuestionSet
{
    public sealed record Query(int Id) : IQuery<QuestionSet?>;
    
    public class Handler : IQueryHandler<Query, QuestionSet?>
    {
        private readonly IQuestionSetRepository _repository;

        public Handler(IQuestionSetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<QuestionSet?>> Handle(Query query, CancellationToken cancellationToken)
        {
            QuestionSet? questionSet = await _repository.GetQuestionSet(query.Id);

            if (questionSet is not null)
            {
                return Result.Ok(questionSet)!;
            }

            return Result.Fail("Questionnaire not found");
        }
    }
}