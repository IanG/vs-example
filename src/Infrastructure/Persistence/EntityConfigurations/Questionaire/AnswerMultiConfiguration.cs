using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class AnswerMultiConfiguration : IEntityTypeConfiguration<AnswerMulti>
{
    public void Configure(EntityTypeBuilder<AnswerMulti> builder)
    {
        builder.ToTable("Answers");

        builder.HasMany(a => a.SelectedOptions)
            .WithMany()
            .UsingEntity(j => j.ToTable("AnswerSelectedOptions"));
    }
}
