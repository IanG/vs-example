namespace VsExample.Application.Features.Questionnaire.Dto;

public class QuestionnaireQueryResult
{
    public IEnumerable<Domain.Entities.Questionaire.Questionnaire> Questionnaires { get; set; } = [];
    public int TotalCount { get; set; }
}