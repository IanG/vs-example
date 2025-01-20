using VsExample.Domain.Entities.Questions;

namespace VSExample.Web.ViewModels.QuestionSets;

public class QuestionSetsIndexViewModel
{
    public string Title { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    public IEnumerable<QuestionSet> QuestionSets { get; set; } = [];
}