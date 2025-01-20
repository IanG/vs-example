using System.Diagnostics.CodeAnalysis;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VsExample.Domain.Entities.Questionaire;
using VsExample.Domain.Entities.Questions;
using VsExample.Infrastructure.Persistence.Questions;
using QuestionGroup = VsExample.Domain.Entities.Questionaire.QuestionGroup;

namespace VsExample.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static void AddApplicationPersistence(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddScoped<IQuestionSetRepository, QuestionSetRepository>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("ProductsDb");
            
            if (environment.IsDevelopment())
                options.EnableSensitiveDataLogging();
        });
        
        // Seed data after the database is created
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        using IServiceScope scope = serviceProvider.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Ensure the database is created
        context.Database.EnsureCreated();

        // Seed the data if needed
        SeedDatabase(context);
    }
    
    private static void SeedDatabase(ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            Faker faker = new Faker();
            
            for (int i = 1; i <= 1000; i++)
            {
                string productName = faker.Commerce.ProductName();
                string productDescription = $"{productName} is a wonderful product";
                
                context.Products.Add(new()
                {
                    Name = productName,
                    Price = decimal.Parse(faker.Commerce.Price()),
                    Description = productDescription,
                    CreatedAt = DateTime.Now
                });    
            }
            
            context.SaveChanges();
        }

        if (!context.Questionnaires.Any())
        {
            // QuestionGroup group1 = new QuestionGroup
            // {
            //     Name = "Personal Information",
            //     Description = "Questions related to personal details.",
            //     Order = 1,
            //     Questions = new List<Question>
            //     {
            //         new Question
            //         {
            //             Text = "What is your name?",
            //             Order = 1,
            //             AnswerType = AnswerTypeEnum.String
            //         },
            //         new Question
            //         {
            //             Text = "How old are you?",
            //             Order = 2,
            //             AnswerType = AnswerTypeEnum.Integer
            //         },
            //         new Question
            //         {
            //             Text = "What is your gender?",
            //             Order = 3,
            //             AnswerType = AnswerTypeEnum.MultiValue,
            //             Options = new List<QuestionOption>
            //             {
            //                 new() { Text = "Male" },
            //                 new() { Text = "Female" },
            //                 new() { Text = "Other" }
            //             }
            //         },
            //         new Question
            //         {
            //             Text = "What is your favorite programming language?",
            //             Order = 4,
            //             AnswerType = AnswerTypeEnum.SingleValue,
            //             Options = new List<QuestionOption>
            //             {
            //                 new () { Text = "C#" },
            //                 new () { Text = "JavaScript" },
            //                 new () { Text = "Java" },
            //                 new () { Text = "C++" }
            //             }
            //         }
            //     }
            // };
            //
            // QuestionGroup group2 = new QuestionGroup
            // {
            //     Name = "Feedback",
            //     Description = "Questions about customer experience.",
            //     Order = 2,
            //     Questions = new List<Question>
            //     {
            //         new Question
            //         {
            //             Text = "How satisfied are you with our service?",
            //             Order = 1,
            //             AnswerType = AnswerTypeEnum.Decimal
            //         },
            //         new Question
            //         {
            //             Text = "What improvements would you like to see?",
            //             Order = 2,
            //             AnswerType = AnswerTypeEnum.String
            //         }
            //     }
            // };
            
            // QuestionGroup group1 = new QuestionGroup
            // {
            //     Name = "Personal Information",
            //     Description = "Questions related to personal details.",
            //     Order = 1,
            //     Questions = new List<Question>
            //     {
            //         new Question
            //         {
            //             Text = "What is your name?",
            //             AnswerType = AnswerTypeEnum.String
            //         },
            //         new Question
            //         {
            //             Text = "How old are you?",
            //             AnswerType = AnswerTypeEnum.Integer
            //         },
            //         new Question
            //         {
            //             Text = "What is your gender?",
            //             AnswerType = AnswerTypeEnum.MultiValue,
            //             Options = new List<QuestionOption>
            //             {
            //                 new() { Text = "Male" },
            //                 new() { Text = "Female" },
            //                 new() { Text = "Other" }
            //             }
            //         }
            //     }
            // };
            //
            // QuestionGroup group2 = new QuestionGroup
            // {
            //     Name = "Feedback",
            //     Description = "Questions about customer experience.",
            //     Order = 2,
            //     Questions = new List<Question>
            //     {
            //         new Question
            //         {
            //             Text = "How satisfied are you with our service?",
            //             AnswerType = AnswerTypeEnum.Decimal
            //         },
            //         new Question
            //         {
            //             Text = "What improvements would you like to see?",
            //             AnswerType = AnswerTypeEnum.String
            //         }
            //     }
            // };
            
            
            Questionnaire questionnaire = new Questionnaire
            {
                Title = "Customer Satisfaction Survey",
                Description = "A survey to collect feedback from customers about their experience.",
                CreatedDate = DateTime.UtcNow,
                QuestionGroups = [
                    new QuestionGroup
                    {
                        Name = "Personal Information",
                        Description = "Questions related to personal details.",
                        Order = 1,
                        Questions = new List<Question>
                        {
                            new Question
                            {
                                Text = "What is your name?",
                                Order = 1,
                                IsRequired = true,
                                AnswerType = AnswerTypeEnum.String
                            },
                            new Question
                            {
                                Text = "How old are you?",
                                Order = 2,
                                IsRequired = true,
                                AnswerType = AnswerTypeEnum.Integer
                            },
                            new Question
                            {
                                Text = "What is your date of birth?",
                                Order = 3,  
                                IsRequired = true,
                                AnswerType = AnswerTypeEnum.DateTime
                            },
                            new Question
                            {
                                Text = "What is your gender?",
                                Order = 4,
                                IsRequired = true,
                                AnswerType = AnswerTypeEnum.SingleValue,
                                Options = new List<QuestionOption>
                                {
                                    new() { Text = "Male", Order = 1},
                                    new() { Text = "Female", Order = 1},
                                    new() { Text = "Other", Order = 3}
                                }
                            },
                            // new Question
                            // {
                            //     Text = "What is your favorite programming language?",
                            //     Order = 5,
                            //     IsRequired = true,
                            //     AnswerType = AnswerTypeEnum.MultiValue,
                            //     Options = new List<QuestionOption>
                            //     {
                            //         new () { Text = "C#", Order = 1},
                            //         new () { Text = "JavaScript", Order = 2 },
                            //         new () { Text = "Java", Order = 3 },
                            //         new () { Text = "C++", Order = 4 }
                            //     }
                            // }
                        }
                    },
                    new QuestionGroup
                    {
                        Name = "Feedback",
                        Description = "Questions about customer experience.",
                        Order = 2,
                        Questions = new List<Question>
                        {
                            new Question
                            {
                                Text = "How satisfied are you with our service?",
                                Order = 1,
                                AnswerType = AnswerTypeEnum.Decimal
                            },
                            new Question
                            {
                                Text = "What improvements would you like to see?",
                                Order = 2,
                                AnswerType = AnswerTypeEnum.String
                            },
                            new Question
                            {
                                Text = "I'd like to join the mailing list",
                                Order = 3,
                                AnswerType = AnswerTypeEnum.Boolean
                            }
                        }
                    }
                ]
            };
            
            context.Questionnaires.Add(questionnaire);
            context.SaveChanges();

        }
    }
}