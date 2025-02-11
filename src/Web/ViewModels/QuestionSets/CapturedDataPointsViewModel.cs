using VsExample.Domain.Entities.Questions;

namespace VSExample.Web.ViewModels.QuestionSets;

public class CapturedDataPointsViewModel
{
    public QuestionSet? QuestionSet { get; init; }
    public Dictionary<string, List<string>> DataPoints { get; init; } = [];

    public List<FileContent> Files { get; init; } = [];

    public class FileContent
    {
        public required string Name { get; init; }
        public required string FileName { get; init; }
        public required string ContentType { get; init; }
        public required byte[] FileData { get; init; }
    }
}