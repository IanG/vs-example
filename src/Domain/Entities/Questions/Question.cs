using System.Text.Json.Serialization;

namespace VsExample.Domain.Entities.Questions;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(StringQuestion), "string")]
[JsonDerivedType(typeof(TextQuestion), "text")]
[JsonDerivedType(typeof(IntegerQuestion), "integer")]
[JsonDerivedType(typeof(DateQuestion), "date")]
[JsonDerivedType(typeof(BooleanQuestion), "boolean")]
[JsonDerivedType(typeof(MultiChoiceQuestion), "multi-choice")]
[JsonDerivedType(typeof(FileUploadQuestion), "file-upload")]
[JsonDerivedType(typeof(RatingQuestion), "rating")]
public abstract class QuestionBase
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(1)]
    public required int Id { get; set; }
    
    [JsonPropertyName("order")]
    [JsonPropertyOrder(2)]
    public required int Order { get; set; }
    
    [JsonPropertyName("prompt")]
    [JsonPropertyOrder(3)]
    public required string Prompt { get; set; } = string.Empty;
    
    [JsonPropertyName("helpText")]
    [JsonPropertyOrder(4)]
    public string? HelpText { get; set; } = string.Empty;
    
    [JsonPropertyName("required")]
    [JsonPropertyOrder(5)]
    public required bool Required { get; set; }
    
    [JsonPropertyName("invalidDataMessage")]
    [JsonPropertyOrder(6)]
    public string? InvalidDataMessage { get; set; }
    
    [JsonPropertyName("capturesField")]
    [JsonPropertyOrder(7)]
    public string? CapturesField { get; set; }
    
    [JsonPropertyName("visbilityRule")]
    [JsonPropertyOrder(8)]
    
    public string? VisbilityRule { get; set; }
}

public class StringQuestion : QuestionBase
{
    [JsonPropertyName("placeholder")]
    [JsonPropertyOrder(9)]
    public string? Placeholder { get; set; }
    
    [JsonPropertyName("minLength")]
    [JsonPropertyOrder(10)]
    public required int MinLength { get; set; }
    
    [JsonPropertyName("maxLength")]
    [JsonPropertyOrder(11)]
    public required int MaxLength { get; set; }
    
    [JsonPropertyName("defaultValue")]
    [JsonPropertyOrder(12)]
    public string? DefaultValue { get; set; } = string.Empty;
    
    [JsonPropertyName("pattern")]
    [JsonPropertyOrder(13)]
    public string? Pattern { get; set; }
}

public class TextQuestion : QuestionBase
{
    [JsonPropertyName("maxLength")]
    [JsonPropertyOrder(9)]
    public required int MaxLength { get; set; }
}

public class IntegerQuestion : QuestionBase
{
    [JsonPropertyName("placeholder")]
    [JsonPropertyOrder(10)]
    public string? Placeholder { get; set; }
    
    [JsonPropertyName("minValue")]
    [JsonPropertyOrder(11)]
    public required int MinValue { get; set; }
    
    [JsonPropertyName("maxValue")]
    [JsonPropertyOrder(12)]
    public required int MaxValue { get; set; }
    
    [JsonPropertyName("defaultValue")]
    [JsonPropertyOrder(13)]
    public int? DefaultValue { get; set; }
}

public class DateQuestion : QuestionBase
{
    [JsonPropertyName("minDate")]
    [JsonPropertyOrder(9)]
    public DateTime? MinDate { get; set; }
    
    [JsonPropertyName("maxDate")]
    [JsonPropertyOrder(10)]
    public DateTime? MaxDate { get; set; }
    
    [JsonPropertyName("mustBeInTheFuture")]
    [JsonPropertyOrder(11)]
    public bool? MustBeInFuture { get; set; }
    
    [JsonPropertyName("mustBeInThePast")]
    [JsonPropertyOrder(12)]
    public bool? MustBeInPast { get; set; }
}

public class BooleanQuestion : QuestionBase
{
    [JsonPropertyName("defaultValue")]
    [JsonPropertyOrder(9)]
    public bool? DefaultValue { get; set; }
}

public class MultiChoiceQuestion : QuestionBase
{
    [JsonPropertyName("options")]
    [JsonPropertyOrder(9)]
    public required List<Option> Options { get; set; } = [];
    
    [JsonPropertyName("allowMultipleSelections")]
    [JsonPropertyOrder(10)]
    public bool AllowMultipleSelections { get; set; } = false;
}

public class FileUploadQuestion : QuestionBase
{
    [JsonPropertyName("allowedFileTypes")]
    [JsonPropertyOrder(9)]
    public List<string> AllowedFileTypes { get; set; } = [];

    [JsonPropertyName("maxFileSizeMb")]
    [JsonPropertyOrder(10)]
    public int? MaxFileSizeMb { get; set; }

    [JsonPropertyName("allowMultiple")]
    [JsonPropertyOrder(11)]
    public bool AllowMultiple { get; set; }
}

public class RatingQuestion : QuestionBase
{
    [JsonPropertyName("minValue")]
    [JsonPropertyOrder(9)]
    public int MinValue { get; set; }

    [JsonPropertyName("maxValue")]
    [JsonPropertyOrder(10)]
    public int MaxValue { get; set; }

    [JsonPropertyName("stepValue")]
    [JsonPropertyOrder(11)]
    public int StepValue { get; set; } = 1; 

    [JsonPropertyName("labels")]
    [JsonPropertyOrder(12)]
    public Dictionary<int, string>? Labels { get; set; } 
}

public class Option
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(1)]
    public int Id { get; set; }
    
    [JsonPropertyName("label")]
    [JsonPropertyOrder(2)]
    public string Label { get; set; } = string.Empty;
    
    [JsonPropertyName("value")]
    [JsonPropertyOrder(3)]
    public string? Value { get; set; }
}