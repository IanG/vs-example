using System.Text.Json;
using System.Text.Json.Serialization;

namespace VsExample.Domain.Entities.Questions;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(StringQuestion), "string")]
[JsonDerivedType(typeof(TextQuestion), "text")]
[JsonDerivedType(typeof(IntegerQuestion), "integer")]
[JsonDerivedType(typeof(DateQuestion), "date")]
[JsonDerivedType(typeof(BooleanQuestion), "boolean")]
[JsonDerivedType(typeof(SingleChoiceQuestion), "single-choice")]
[JsonDerivedType(typeof(MultiChoiceQuestion), "multi-choice")]
[JsonDerivedType(typeof(FileUploadQuestion), "file-upload")]
[JsonDerivedType(typeof(RatingQuestion), "rating")]
public abstract class QuestionBase
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(1)]
    public required int Id { get; init; }
    
    [JsonPropertyName("order")]
    [JsonPropertyOrder(2)]
    public required int Order { get; init; }
    
    [JsonPropertyName("prompt")]
    [JsonPropertyOrder(3)]
    public required string Prompt { get; init; } = string.Empty;
    
    [JsonPropertyName("helpText")]
    [JsonPropertyOrder(4)]
    public string? HelpText { get; init; } = string.Empty;
    
    [JsonPropertyName("required")]
    [JsonPropertyOrder(5)]
    public required bool Required { get; init; }
    
    [JsonPropertyName("invalidDataMessage")]
    [JsonPropertyOrder(6)]
    public string? InvalidDataMessage { get; init; }
    
    [JsonPropertyName("capturesField")]
    [JsonPropertyOrder(7)]
    public string? CapturesField { get; init; }
    
    [JsonPropertyName("visibilityRule")]
    [JsonPropertyOrder(8)]
    public string? VisibilityRule { get; init; }
}

public class StringQuestion : QuestionBase
{
    [JsonPropertyName("placeholder")]
    [JsonPropertyOrder(9)]
    public string? Placeholder { get; init; }
    
    [JsonPropertyName("minLength")]
    [JsonPropertyOrder(10)]
    public required int MinLength { get; init; }
    
    [JsonPropertyName("maxLength")]
    [JsonPropertyOrder(11)]
    public required int MaxLength { get; init; }
    
    [JsonPropertyName("defaultValue")]
    [JsonPropertyOrder(12)]
    public string? DefaultValue { get; init; } = string.Empty;
    
    [JsonPropertyName("pattern")]
    [JsonPropertyOrder(13)]
    public string? Pattern { get; init; }
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
    public string? Placeholder { get; init; }
    
    [JsonPropertyName("minValue")]
    [JsonPropertyOrder(11)]
    public required int MinValue { get; init; }
    
    [JsonPropertyName("maxValue")]
    [JsonPropertyOrder(12)]
    public required int MaxValue { get; init; }
    
    [JsonPropertyName("defaultValue")]
    [JsonPropertyOrder(13)]
    public int? DefaultValue { get; set; }
}

public class DateQuestion : QuestionBase
{
    [JsonPropertyName("minDate")]
    [JsonPropertyOrder(9)]
    public DateTime? MinDate { get; init; }
    
    [JsonPropertyName("maxDate")]
    [JsonPropertyOrder(10)]
    public DateTime? MaxDate { get; init; }
    
    [JsonPropertyName("mustBeInTheFuture")]
    [JsonPropertyOrder(11)]
    public bool? MustBeInFuture { get; init; }
    
    [JsonPropertyName("mustBeInThePast")]
    [JsonPropertyOrder(12)]
    public bool? MustBeInPast { get; set; }
}

public class BooleanQuestion : QuestionBase
{
    [JsonPropertyName("defaultValue")]
    [JsonPropertyOrder(9)]
    public bool? DefaultValue { get; init; }
}

public class SingleChoiceQuestion : QuestionBase
{
    [JsonPropertyName("placeholder")]
    [JsonPropertyOrder(9)]
    public string? Placeholder { get; init; }
    
    [JsonPropertyName("options")]
    [JsonPropertyOrder(10)]
    public required List<Option> Options { get; init; } = [];
    
    [JsonPropertyName("selectionMode")]
    [JsonConverter(typeof(SingleChoiceSelectionModeConverter))] // Apply custom converter
    [JsonPropertyOrder(11)]
    public SingleChoiceSelectionMode SelectionMode { get; init; } = SingleChoiceSelectionMode.Dropdown;
}

public class MultiChoiceQuestion : QuestionBase
{
    [JsonPropertyName("options")]
    [JsonPropertyOrder(9)]
    public required List<Option> Options { get; init; } = [];
    
    [JsonPropertyName("allowMultipleSelections")]
    [JsonPropertyOrder(10)]
    public bool AllowMultipleSelections { get; init; } = false;
}

public class FileUploadQuestion : QuestionBase
{
    [JsonPropertyName("allowedFileTypes")]
    [JsonPropertyOrder(9)]
    public List<string> AllowedFileTypes { get; init; } = [];

    [JsonPropertyName("maxFileSizeMb")]
    [JsonPropertyOrder(10)]
    public int? MaxFileSizeMb { get; init; }

    [JsonPropertyName("allowMultiple")]
    [JsonPropertyOrder(11)]
    public bool AllowMultiple { get; init; }
}

public class RatingQuestion : QuestionBase
{
    [JsonPropertyName("minValue")]
    [JsonPropertyOrder(9)]
    public int MinValue { get; init; }

    [JsonPropertyName("maxValue")]
    [JsonPropertyOrder(10)]
    public int MaxValue { get; init; }

    [JsonPropertyName("stepValue")]
    [JsonPropertyOrder(11)]
    public int StepValue { get; init; } = 1; 

    [JsonPropertyName("labels")]
    [JsonPropertyOrder(12)]
    public Dictionary<int, string>? Labels { get; init; } 
}

public class Option
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(1)]
    public int Id { get; init; }
    
    [JsonPropertyName("label")]
    [JsonPropertyOrder(2)]
    public required string Label { get; init; } = string.Empty;
    
    [JsonPropertyName("value")]
    [JsonPropertyOrder(3)]
    public required string Value { get; init; }
}

public enum SingleChoiceSelectionMode
{
    
    Dropdown = 1,
    Radio = 2,
}

public class SingleChoiceSelectionModeConverter : JsonConverter<SingleChoiceSelectionMode>
{
    public override SingleChoiceSelectionMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        return value switch
        {
            "dropdown" => SingleChoiceSelectionMode.Dropdown,
            "radio" => SingleChoiceSelectionMode.Radio,
            _ => throw new ArgumentException($"Unknown value for SelectionMode: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, SingleChoiceSelectionMode value, JsonSerializerOptions options)
    {
        string stringValue = value switch
        {
            SingleChoiceSelectionMode.Dropdown => "dropdown",
            SingleChoiceSelectionMode.Radio => "radio",
            _ => throw new ArgumentOutOfRangeException()
        };

        writer.WriteStringValue(stringValue);
    }
}