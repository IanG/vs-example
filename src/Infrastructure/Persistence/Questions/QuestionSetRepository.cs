using VsExample.Domain.Entities.Questions;

namespace VsExample.Infrastructure.Persistence.Questions;

public class QuestionSetRepository : IQuestionSetRepository
{
    private readonly List<QuestionSet> _questionSets =
    [
        new()
        {
          Id = 3,
          Name = "Question Set Testing Visibility Rules",
          Version = 1,
          Description = "This Question Set is purely for testing Testing Visibility Rules",
          QuestionGroups = [
              new()
              {
                    Id = 1,
                    Name = "Question Group 1",
                    Order = 1,
                    Description = "This is Question Group 1",
                    Questions = [
                        new StringQuestion() { Id = 1, Order = 1, CapturesField = "Name",Prompt = "What is your name?", Placeholder = "Name", MinLength = 1, MaxLength = 100, HelpText = "Tell us your name", Required = true, InvalidDataMessage = "Name required"},
                        new IntegerQuestion() { Id = 2, Order = 2, CapturesField = "FaveNumber", Prompt = "What is your fave number between 1 and 10?", Placeholder = "Fave Number", MinValue = 1, MaxValue = 10, DefaultValue = 1, HelpText = "Pick a number, depending how you answer could give you more questions to complete.", Required = true, InvalidDataMessage = "We need to know your fave number."},
                        new SingleChoiceQuestion() { Id = 3, Order = 3, CapturesField = "FaveFruit", Prompt = "What is your fave fruit ?", Placeholder = "Select a fruit...", Required = true, HelpText = "Select a fruite from the list", InvalidDataMessage = "No fruit selected", Options = [
                                new() { Id = 1, Label = "🍎 Apple", Value = "Apple" },
                                new() { Id = 2, Label = "🍑 Orange", Value = "Orange" },
                                new() { Id = 3, Label = "🍌 Banana", Value = "Banana" },
                                new() { Id = 4, Label = "🍇 Grapes", Value = "Grapes" },
                        ], SelectionMode = SingleChoiceSelectionMode.Dropdown},
                        new SingleChoiceQuestion() { Id = 4, Order = 4, CapturesField = "FaveShape", Prompt = "What is your fave shape ?", Placeholder = "Select a shape...", Required = true, HelpText = "Select a shape from the list", InvalidDataMessage = "No shape selected", Options = [
                            new() { Id = 1, Label = "🔺 Triangle", Value = "Triangle" },
                            new() { Id = 2, Label = "🟩 Square", Value = "Square" },
                            new() { Id = 3, Label = "🟠 Circle", Value = "Circle" },
                            new() { Id = 4, Label = "🔷 Diamond", Value = "Diamond" },
                        ], SelectionMode = SingleChoiceSelectionMode.Radio},
                        new StringQuestion() { Id = 5, Order = 5, CapturesField = "GrapesReason",Prompt = "Tell us why you like 🍇 grapes so much?", Placeholder = "Explain why you like grapes", MinLength = 1, MaxLength = 100, HelpText = "Give us some more information about your love of grapes", Required = true, InvalidDataMessage = "Please provide the reason why you love grapes", VisbilityRule = "FaveFruit == 'Grapes'"},
                        new StringQuestion() { Id = 6, Order = 6, CapturesField = "WhyYouLoveMumber7", Prompt = "Seems like you like the number 7, tell us why", Placeholder = "Why do you love number 7 ?", MinLength = 1, MaxLength = 7, HelpText = "Tell us why you love the number 7", Required = true, InvalidDataMessage = "Give us the reason", VisbilityRule = "FaveNumber == 7"},
                        new StringQuestion() { Id = 7, Order = 7, CapturesField = "WhyYouLoveMumber7Additional", Prompt = "Tell us more about the number 7 !!!", Placeholder = "Big up the number 7", MinLength = 1, MaxLength = 2, HelpText = "Tell us more about your love of the number 7", Required = true, InvalidDataMessage = "Give us the reason", VisbilityRule = "FaveNumber == '7'"},
                        new StringQuestion() { Id = 8, Order = 8, CapturesField = "Number5Answer", Prompt = "Seems like you prefer the number 5 - tell us why", Placeholder = "Explain why you prefer the number 5", MinLength = 1, MaxLength = 2, HelpText = "Ok, so you prefer number 5 to number 7 - tell us why", Required = true, InvalidDataMessage = "Please complete this field", VisbilityRule = "FaveNumber == '5'"}
                    ]
              },
              new()
              {
                  Id = 2,
                  Name = "Question Group 2",
                  Order = 2,
                  Description = "This is Question Group 2",
                  Questions = [
                      new StringQuestion() { Id = 1, Order = 1, CapturesField = "PetName",Prompt = "What is your name of your pet?", Placeholder = "Your pets name", MinLength = 1, MaxLength = 100, HelpText = "Tell us your pet's name", Required = true, InvalidDataMessage = "Name required", VisbilityRule = "FaveNumber == 7" },
                      new StringQuestion() { Id = 2, Order = 2, CapturesField = "PetName2ndChoice",Prompt = "Dave is a GREAT name for a dog.  What was your 2nd choice?", Placeholder = "2nd choice for pet name", MinLength = 1, MaxLength = 100, HelpText = "Tell us your pet's name", Required = true, InvalidDataMessage = "Name required", VisbilityRule = "PetName == 'Dave'" },
                  ]
              }
          ]
        },
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
                        new StringQuestion() { Id = 1, Order = 1, CapturesField = "Name",Prompt = "What is your name?", Placeholder = "Name", MinLength = 1, MaxLength = 100, HelpText = "Tell us your name", Required = true, InvalidDataMessage = "Name required"},
                        new BooleanQuestion() { Id = 2, Order = 2, CapturesField = "FirstRodeo", Prompt = "Is this your first rodeo?", DefaultValue = false, HelpText = "If you are experienced in rodeo, you should answer no", Required = true, InvalidDataMessage = "Rodeo experience required" },
                        new IntegerQuestion() { Id = 3, Order = 3, CapturesField = "FaveNumber", Prompt = "What is your fave number between 1 and 10?", Placeholder = "Fave Number", MinValue = 1, MaxValue = 10, DefaultValue = 1, HelpText = "Pick a number", Required = true, InvalidDataMessage = "We need to know your fave number."},
                        new RatingQuestion()
                        {
                            Id = 4, Order = 4, CapturesField = "LifeRating",Prompt = "How do you rate life?", MinValue = 1, MaxValue = 3, StepValue = 1 , Labels = new()
                            {
                                { 1, "Poor" },
                                { 2, "Good" },
                                { 3, "Amazing" },
                            },
                            HelpText = "Tell us how you feel about life in general",
                            Required = true,
                            InvalidDataMessage = "We need to know how you feel about life"
                        },
                        new StringQuestion() { Id = 5, Order = 5, CapturesField = "NINumber", Prompt = "What is your National Insurance Number?", Placeholder = "National Insurance Number", MinLength = 7, MaxLength = 7, HelpText = "Give us your NI Number", /*Pattern = "[a-zA-Z]{2}\\d{6}[a-zA-Z]",*/ Required = true, InvalidDataMessage = "We need your national insurance number"}
                    ]
                },
                new()
                {
                    Id = 2,
                    Name = "Question Group 2",
                    Order = 2,
                    Description = "This is Question Group 2",
                    Questions = [
                        new DateQuestion() { Id = 1, Order = 1, CapturesField = "DOB", Prompt = "What is your date of birth", MustBeInPast = true, HelpText = "It was the day you came to be, tell us", Required = true, InvalidDataMessage = "Date of birth required"},
                        new FileUploadQuestion()
                        {
                            Id = 2, Order = 2, CapturesField = "ProfilePicture", Prompt = "Upload your profile picture", AllowedFileTypes = ["application/jpg", "application/png"], 
                            AllowMultiple = false, MaxFileSizeMb = 2, 
                            HelpText = "You can upload a file to use as your profile picture", 
                            Required = true,
                            InvalidDataMessage = "You need to upload your profile picture"
                        },
                        new MultiChoiceQuestion()
                        {
                            Id = 3, 
                            Order = 3, 
                            CapturesField = "FaveColours",
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
                        new StringQuestion() { Id = 1, Order = 1, CapturesField = "Name", Prompt = "What is your name?", Placeholder = "Name", MinLength = 1, MaxLength = 100, HelpText = "Tell us your name", Required = true , InvalidDataMessage = "You must provide your name"},
                        new BooleanQuestion() { Id = 2, Order = 2, CapturesField = "FirstRodeo", Prompt = "Is this your first rodeo?", DefaultValue = false, HelpText = "If you are experienced in rodeo, you should answer no", Required = true, InvalidDataMessage = "please tell us if this is your first rodeo or not" },
                        new IntegerQuestion() { Id = 3, Order = 3, CapturesField = "FaveNumber", Prompt = "What is your fave number between 1 and 10?", Placeholder = "Fave Number", MinValue = 1, MaxValue = 10, DefaultValue = 1, HelpText = "Pick a number", Required = true, InvalidDataMessage = "Please tell us your fave number"},
                        new RatingQuestion()
                        {
                            Id = 4, Order = 4, CapturesField = "LifeRating", Prompt = "How do you rate life?", MinValue = 1, MaxValue = 3, StepValue = 1 , Labels = new()
                            {
                                { 1, "Poor" },
                                { 2, "Good" },
                                { 3, "Amazing" },
                            },
                            HelpText = "Tell us how you feel about life in general",
                            Required = true,
                            InvalidDataMessage = "please rate your life in general"
                        },
                        new StringQuestion() { Id = 5, Order = 5, CapturesField = "NINumber", Prompt = "What is your National Insurance Number?", Placeholder = "National Insurance Number", MinLength = 7, MaxLength = 7, HelpText = "Give us your NI Number", /*Pattern = "[a-zA-Z]{2}\\d{6}[a-zA-Z]",*/ Required = true, InvalidDataMessage = "National insurance number required"}
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