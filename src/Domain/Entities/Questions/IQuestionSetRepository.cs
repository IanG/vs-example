namespace VsExample.Domain.Entities.Questions;

public interface IQuestionSetRepository
{
    public Task<QuestionSet?> GetQuestionSet(int questionSetId);
    public Task<IEnumerable<QuestionSet>> GetQuestionSets();
}