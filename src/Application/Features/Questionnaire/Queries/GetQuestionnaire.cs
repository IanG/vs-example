using FluentResults;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Application.Features.Questionnaire.Queries;

public class GetQuestionnaire
{
    public sealed record Query(int Id) : IQuery<Domain.Entities.Questionaire.Questionnaire?>;

    public class Handler : IQueryHandler<Query, Domain.Entities.Questionaire.Questionnaire?>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Domain.Entities.Questionaire.Questionnaire?>> Handle(Query query, CancellationToken cancellationToken)
        {
            Domain.Entities.Questionaire.Questionnaire? questionnaire = await _dbContext.Questionnaires
                .Include(q => q.QuestionGroups.OrderBy(qg => qg.Order))
                .ThenInclude(qg => qg.Questions.OrderBy(q => q.Order))
                .ThenInclude(q => q.Options.OrderBy(o  => o.Order))
                .FirstOrDefaultAsync(q => q.Id == query.Id, cancellationToken);

            if (questionnaire is not null)
            {
                return Result.Ok(questionnaire)!;
            }

            return Result.Fail("Questionnaire not found");
        }
    }
}