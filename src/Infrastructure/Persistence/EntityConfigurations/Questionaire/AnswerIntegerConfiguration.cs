using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class AnswerIntegerConfiguration : IEntityTypeConfiguration<AnswerInteger>
{
    public void Configure(EntityTypeBuilder<AnswerInteger> builder)
    {
        builder.ToTable("Answers");
        builder.Property(a => a.Value)
            .HasColumnName("ValueInteger")
            .IsRequired(false);
    }
}
