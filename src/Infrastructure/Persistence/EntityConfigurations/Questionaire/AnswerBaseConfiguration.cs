using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations.Questionaire;

public class AnswerBaseConfiguration : IEntityTypeConfiguration<AnswerBase>
{
    public void Configure(EntityTypeBuilder<AnswerBase> builder)
    {
        builder.ToTable("Answers");

        builder.HasDiscriminator(a => a.AnswerType)
            .HasValue<AnswerString>(AnswerTypeEnum.String)
            .HasValue<AnswerInteger>(AnswerTypeEnum.Integer)
            .HasValue<AnswerDecimal>(AnswerTypeEnum.Decimal)
            .HasValue<AnswerBoolean>(AnswerTypeEnum.Boolean)
            .HasValue<AnswerDateTime>(AnswerTypeEnum.DateTime)
            .HasValue<AnswerMulti>(AnswerTypeEnum.MultiValue)
            .HasValue<AnswerSingle>(AnswerTypeEnum.SingleValue);
        
        builder.HasKey(a => a.Id); 

        builder.HasOne(a => a.Question)
            .WithMany()
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade); // Ensure deletion cascade if necessary
    }
}
