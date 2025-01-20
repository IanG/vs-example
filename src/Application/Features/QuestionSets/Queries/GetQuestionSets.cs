using FluentResults;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Application.Features.QuestionSets.Dto;
using VsExample.Domain.Entities.Questions;

namespace VsExample.Application.Features.QuestionSets.Queries;

public class GetQuestionSets
{
    public sealed record Query(int PageNumber = 1, int PageSize = 10) : IQuery<QuestionSetsQueryResult>;
    
    public class Handler : IQueryHandler<Query, QuestionSetsQueryResult>
    {
        private readonly IQuestionSetRepository _repository;

        public Handler(IQuestionSetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<QuestionSetsQueryResult>> Handle(Query query, CancellationToken cancellationToken)
        {
            IEnumerable<QuestionSet> questionSets = await _repository.GetQuestionSets();
            
            return Result.Ok(new QuestionSetsQueryResult
            {
                QuestionSets = questionSets,
                TotalCount = 1
            });
        }
    }
}