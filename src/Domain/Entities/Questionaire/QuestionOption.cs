namespace VsExample.Domain.Entities.Questionaire;

public class QuestionOption
{
    public int Id { get; init; }
    public string Text { get; init; }  // Display text for the option
    public int Order { get; init; }
    public int QuestionId { get; init; }
    public Question Question { get; init; }
}