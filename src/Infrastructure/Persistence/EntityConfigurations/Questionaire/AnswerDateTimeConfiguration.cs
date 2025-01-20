using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class AnswerDateTimeConfiguration : IEntityTypeConfiguration<AnswerDateTime>
{
    public void Configure(EntityTypeBuilder<AnswerDateTime> builder)
    {
        builder.ToTable("Answers");
        builder.Property(a => a.Value)
            .HasColumnName("ValueDateTime")
            .HasColumnType("datetime")
            .IsRequired(false);
    }
}
