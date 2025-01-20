namespace VsExample.Domain.Entities.Questionaire;

public class QuestionGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; } // Order of the group within the questionnaire

    public int QuestionnaireId { get; set; }  // Foreign key to Questionnaire
    public Questionnaire Questionnaire { get; set; }  // Navigation property to Questionnaire
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
