using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class AnswerStringConfiguration : IEntityTypeConfiguration<AnswerString>
{
    public void Configure(EntityTypeBuilder<AnswerString> builder)
    {
        builder.ToTable("Answers");
        
        builder.Property(a => a.Value)
            .HasColumnName("ValueString") 
            .HasMaxLength(255)
            .IsRequired(false);
    }
}
