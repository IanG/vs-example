using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class AnswerDecimalConfiguration : IEntityTypeConfiguration<AnswerDecimal>
{
    public void Configure(EntityTypeBuilder<AnswerDecimal> builder)
    {
        builder.ToTable("Answers");
        builder.Property(a => a.Value)
            .HasColumnName("ValueDecimal")
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);
    }
}
