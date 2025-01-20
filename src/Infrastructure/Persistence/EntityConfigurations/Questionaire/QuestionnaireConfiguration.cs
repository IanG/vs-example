using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
{
    public void Configure(EntityTypeBuilder<Questionnaire> builder)
    {
        builder.ToTable("Questionnaires");

        builder.HasKey(q => q.Id);
        builder.Property(q => q.Title).IsRequired().HasMaxLength(255); // Title is required
        builder.Property(q => q.Description).HasMaxLength(1000); // Optional description
        builder.Property(q => q.CreatedDate).IsRequired(); // CreatedDate is required

        // Configure one-to-many relationship between Questionnaire and QuestionGroup
        builder.HasMany(q => q.QuestionGroups)
            .WithOne(qg => qg.Questionnaire)
            .HasForeignKey(qg => qg.QuestionnaireId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete when questionnaire is deleted
    }
}
