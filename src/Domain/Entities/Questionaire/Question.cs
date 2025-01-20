namespace VsExample.Domain.Entities.Questionaire;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }  // The question text
    
    public int Order { get; set; }

    public AnswerTypeEnum AnswerType { get; set; }  // Specifies the type of answer expected
    public bool IsRequired { get; set; }
    
    public int QuestionGroupId { get; set; }  // FK to QuestionGroup
    public QuestionGroup QuestionGroup { get; set; }  // Navigation property

    public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();  // Possible options for this question
}