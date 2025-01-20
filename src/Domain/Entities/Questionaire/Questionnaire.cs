namespace VsExample.Domain.Entities.Questionaire;

public class Questionnaire
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<QuestionGroup> QuestionGroups { get; set; } = new List<QuestionGroup>();
}
