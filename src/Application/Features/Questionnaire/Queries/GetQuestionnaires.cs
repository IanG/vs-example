using System.Diagnostics;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Application.Features.Questionnaire.Dto;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Application.Features.Questionnaire.Queries;

public class GetQuestionnaires
{
    public sealed record Query(string? Filter = null, int PageNumber = 1, int PageSize = 10) : IQuery<QuestionnaireQueryResult>;
    
    public class Handler : IQueryHandler<Query, QuestionnaireQueryResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<QuestionnaireQueryResult>> Handle(Query query, CancellationToken cancellationToken)
        {
            int skip = (query.PageNumber - 1) * query.PageSize;
            int totalCount = await _dbContext.Questionnaires.CountAsync(cancellationToken);
            
            IEnumerable<Domain.Entities.Questionaire.Questionnaire> questionnaires = await _dbContext.Questionnaires
                .Skip(skip)
                .Take(query.PageSize)
                .Include(q => q.QuestionGroups.OrderBy(qg => qg.Order))
                .ThenInclude(qg => qg.Questions.OrderBy(qs => qs.Order))
                .ThenInclude(q => q.Options.OrderBy(qo => qo.Order))
                .ToListAsync(cancellationToken);
            
            return Result.Ok(new QuestionnaireQueryResult
            {
                TotalCount = totalCount,
                Questionnaires = questionnaires
            });
        }
    }
}