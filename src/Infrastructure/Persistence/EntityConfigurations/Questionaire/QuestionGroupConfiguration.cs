using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class QuestionGroupConfiguration : IEntityTypeConfiguration<QuestionGroup>
{
    public void Configure(EntityTypeBuilder<QuestionGroup> builder)
    {
        builder.ToTable("QuestionGroups");

        builder.HasKey(qg => qg.Id);
        builder.Property(qg => qg.Description).IsRequired().HasMaxLength(500);
        builder.Property(qg => qg.Name).IsRequired().HasMaxLength(255); // Name is required
        builder.Property(qg => qg.Description).HasMaxLength(1000); // Optional description
        builder.Property(qg => qg.Order)
            .HasColumnName("Order")
            .IsRequired(); // Order for display
        
        // Ensure unique Order within a Questionnaire
        builder.HasIndex(qg => new { qg.QuestionnaireId, qg.Order })
            .IsUnique()
            .HasDatabaseName("IX_QuestionGroup_Questionnaire_Order");

        // Configure many-to-one relationship from QuestionGroup to Questionnaire
        builder.HasOne(qg => qg.Questionnaire)
            .WithMany(q => q.QuestionGroups)
            .HasForeignKey(qg => qg.QuestionnaireId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for groups when questionnaire is deleted

        // Configure relationship with Question
        builder.HasMany(qg => qg.Questions)
            .WithOne(q => q.QuestionGroup)
            .HasForeignKey(q => q.QuestionGroupId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for questions if group is deleted
    }
}

