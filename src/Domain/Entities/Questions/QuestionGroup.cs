using System.Text.Json.Serialization;

namespace VsExample.Domain.Entities.Questions;

public class QuestionGroup
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("order")]
    public int Order { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("questions")]
    public List<QuestionBase> Questions { get; set; } = new();
}