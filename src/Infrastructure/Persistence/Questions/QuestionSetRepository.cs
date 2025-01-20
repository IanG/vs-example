using VsExample.Domain.Entities.Questions;

namespace VsExample.Infrastructure.Persistence.Questions;

public class QuestionSetRepository : IQuestionSetRepository
{
    private readonly List<QuestionSet> _questionSets =
    [
        new()
        {
            Id = 1,
            Name = "Question Set 1",
            Version = 1,
            Description = "This is Question Set 1",
            QuestionGroups =
            [
                new()
                {
                    Id = 1,
                    Name = "Question Group 1",
                    Order = 1,
                    Description = "This is Question Group 1",
                    Questions = [
                        new StringQuestion() { Id = 1, Order = 1, Prompt = "What is your name?", Placeholder = "Name", MinLength = 1, MaxLength = 100, HelpText = "Tell us your name", Required = true, InvalidDataMessage = "Name required"},
                        new BooleanQuestion() { Id = 2, Order = 2, Prompt = "Is this your first rodeo?", DefaultValue = false, HelpText = "If you are experienced in rodeo, you should answer no", Required = true, InvalidDataMessage = "Rodeo experience required" },
                        new IntegerQuestion() { Id = 3, Order = 3, Prompt = "What is your fave number between 1 and 10?", Placeholder = "Fave Number", MinValue = 1, MaxValue = 10, DefaultValue = 1, HelpText = "Pick a number", Required = true, InvalidDataMessage = "We need to know your fave number."},
                        new RatingQuestion()
                        {
                            Id = 4, Order = 4, Prompt = "How do you rate life?", MinValue = 1, MaxValue = 3, StepValue = 1 , Labels = new()
                            {
                                { 1, "Poor" },
                                { 2, "Good" },
                                { 3, "Amazing" },
                            },
                            HelpText = "Tell us how you feel about life in general",
                            Required = true,
                            InvalidDataMessage = "We need to know how you feel about life"
                        },
                        new StringQuestion() { Id = 5, Order = 5, Prompt = "What is your National Insurance Number?", Placeholder = "National Insurance Number", MinLength = 7, MaxLength = 7, HelpText = "Give us your NI Number", /*Pattern = "[a-zA-Z]{2}\\d{6}[a-zA-Z]",*/ Required = true, InvalidDataMessage = "We need your national insurance number"}
                    ]
                },
                new()
                {
                    Id = 2,
                    Name = "Question Group 2",
                    Order = 2,
                    Description = "This is Question Group 2",
                    Questions = [
                        new DateQuestion() { Id = 1, Order = 1, Prompt = "What is your date of birth", MustBeInPast = true, HelpText = "It was the day you came to be, tell us", Required = true, InvalidDataMessage = "Date of birth required"},
                        new FileUploadQuestion()
                        {
                            Id = 2, Order = 2, Prompt = "Upload your profile picture", AllowedFileTypes = ["application/jpg", "application/png"], 
                            AllowMultiple = false, MaxFileSizeMb = 2, 
                            HelpText = "You can upload a file to use as your profile picture", 
                            Required = true,
                            InvalidDataMessage = "You need to upload your profile picture"
                        },
                        new MultiChoiceQuestion()
                        {
                            Id = 3, 
                            Order = 3, 
                            Prompt = "Pick your fave colours", 
                            Options = [ 
                                new() { Id = 1, Label = "Green", Value = "Green" },
                                new() { Id = 2, Label = "Blue", Value = "Blue" },
                            ], 
                            HelpText = "Pick your fave colours from our list",
                            Required = true,
                            InvalidDataMessage = "Pick your fave colours"
                        }
                    ]
                }
            ]
        },
        new()
        {
            Id = 2,
            Name = "Question Set 2",
            Version = 1,
            Description = "This is Question Set 2",
            QuestionGroups =
            [
                new()
                {
                    Id = 1,
                    Name = "Question Group 1",
                    Order = 1,
                    Description = "This is Question Group 1",
                    Questions = [
                        new StringQuestion() { Id = 1, Order = 1, Prompt = "What is your name?", Placeholder = "Name", MinLength = 1, MaxLength = 100, HelpText = "Tell us your name", Required = true , InvalidDataMessage = "You must provide your name"},
                        new BooleanQuestion() { Id = 2, Order = 2, Prompt = "Is this your first rodeo?", DefaultValue = false, HelpText = "If you are experienced in rodeo, you should answer no", Required = true, InvalidDataMessage = "please tell us if this is your first rodeo or not" },
                        new IntegerQuestion() { Id = 3, Order = 3, Prompt = "What is your fave number between 1 and 10?", Placeholder = "Fave Number", MinValue = 1, MaxValue = 10, DefaultValue = 1, HelpText = "Pick a number", Required = true, InvalidDataMessage = "Please tell us your fave number"},
                        new RatingQuestion()
                        {
                            Id = 4, Order = 4, Prompt = "How do you rate life?", MinValue = 1, MaxValue = 3, StepValue = 1 , Labels = new()
                            {
                                { 1, "Poor" },
                                { 2, "Good" },
                                { 3, "Amazing" },
                            },
                            HelpText = "Tell us how you feel about life in general",
                            Required = true,
                            InvalidDataMessage = "please rate your life in general"
                        },
                        new StringQuestion() { Id = 5, Order = 5, Prompt = "What is your National Insurance Number?", Placeholder = "National Insurance Number", MinLength = 7, MaxLength = 7, HelpText = "Give us your NI Number", /*Pattern = "[a-zA-Z]{2}\\d{6}[a-zA-Z]",*/ Required = true, InvalidDataMessage = "National insurance number required"}
                    ]
                }
            ]
        }    
    ];
    
    public Task<QuestionSet?> GetQuestionSet(int questionSetId)
    {
        return Task.FromResult(_questionSets.SingleOrDefault(qs => qs.Id == questionSetId));
    }

    public Task<IEnumerable<QuestionSet>> GetQuestionSets()
    {
        return Task.FromResult<IEnumerable<QuestionSet>>(_questionSets);
    }
}