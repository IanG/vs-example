using VsExample.Domain.Entities.Questionaire;

namespace VSExample.Web.ViewModels.Questionnaires;

public class QuestionnairesIndexViewModel
{
    public string Title { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    public IEnumerable<Questionnaire> Questionnaires { get; set; } = [];
}