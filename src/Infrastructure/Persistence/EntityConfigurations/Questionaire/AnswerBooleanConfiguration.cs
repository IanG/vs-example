using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class AnswerBooleanConfiguration : IEntityTypeConfiguration<AnswerBoolean>
{
    public void Configure(EntityTypeBuilder<AnswerBoolean> builder)
    {
        builder.ToTable("Answers");
        builder.Property(a => a.Value)
            .HasColumnName("ValueBoolean")
            .IsRequired();
    }
}
