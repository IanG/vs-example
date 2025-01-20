namespace VsExample.Domain.Entities.Questionaire;

public class QuestionOption
{
    public int Id { get; set; }
    public string Text { get; set; }  // Display text for the option
    
    public int Order { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}