using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Text)
            .IsRequired()
            .HasColumnName("Text")
            .HasMaxLength(1000); 
        
        builder.Property(q => q.AnswerType).IsRequired(); // AnswerTypeEnum is required
        
        builder.Property(q => q.Order)
            .IsRequired()
            .HasColumnName("Order");
        
        // Optional: Configure indexing for Order within a group
        builder.HasIndex(q => new { q.QuestionGroupId, q.Order })
            .IsUnique(); // Prevent duplicate Order values within the same group

        // Configure many-to-one relationship from Question to QuestionGroup
        builder.HasOne(q => q.QuestionGroup)
            .WithMany(qg => qg.Questions)
            .HasForeignKey(q => q.QuestionGroupId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for questions if group is deleted

        // Configure relationship with QuestionOption
        builder.HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

