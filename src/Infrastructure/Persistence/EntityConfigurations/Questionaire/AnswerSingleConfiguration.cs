namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

public class AnswerSingleConfiguration : IEntityTypeConfiguration<AnswerSingle>
{
    public void Configure(EntityTypeBuilder<AnswerSingle> builder)
    {
        builder.ToTable("Answers");

        // Configure discriminator
        builder.HasDiscriminator(a => a.AnswerType)
            .HasValue<AnswerSingle>(AnswerTypeEnum.SingleValue);

        // Configure foreign key relationship for SelectedOption
        builder.HasOne(a => a.SelectedOption)
            .WithMany() // No navigation property in QuestionOption
            .HasForeignKey("SelectedOptionId") // Use a shadow property for the FK
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

        // Optionality for the SelectedOption
        builder.Property<int?>("SelectedOptionId").IsRequired(false);

        // Base class properties
        builder.Property(a => a.Id).IsRequired();
        builder.Property(a => a.QuestionId).IsRequired();
    }
}
