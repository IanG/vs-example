using System.Text.Json.Serialization;

namespace VsExample.Domain.Entities.Questions;

public class QuestionSet
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("version")]
    public int Version { get; set; }
    
    [JsonPropertyName("questionGroups")]
    public List<QuestionGroup> QuestionGroups { get; set; } = new();
}