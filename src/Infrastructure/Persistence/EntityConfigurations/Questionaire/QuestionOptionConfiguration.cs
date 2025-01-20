using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable("QuestionOptions");
        
        builder.Property(qo => qo.Text)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(qo => qo.Order)
            .IsRequired(); 
        
        // Enforce unique constraint on (QuestionId, Order)
        builder.HasIndex(qo => new { qo.QuestionId, qo.Order })
            .IsUnique();

        builder.HasOne(qo => qo.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(qo => qo.QuestionId)
            .OnDelete(DeleteBehavior.Cascade); // Ensure deletion cascade if necessary
    }
}
