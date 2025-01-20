namespace VsExample.Domain.Entities.Questionaire; 

public abstract class AnswerBase
{
    public int Id { get; set; }
    public AnswerTypeEnum AnswerType { get; set; }  // Enum discriminator
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    
    protected AnswerBase(AnswerTypeEnum answerType)
    {
        AnswerType = answerType;
    }
}

public class AnswerString : AnswerBase
{
    public string Value { get; set; }

    public AnswerString() : base(AnswerTypeEnum.String) { }
}

public class AnswerInteger : AnswerBase
{
    public int? Value { get; set; }
    
    public AnswerInteger() : base(AnswerTypeEnum.Integer) { }
}

public class AnswerDecimal : AnswerBase
{
    public decimal? Value { get; set; }
    
    public AnswerDecimal() : base(AnswerTypeEnum.Decimal) { }
}

public class AnswerBoolean : AnswerBase
{
    public bool Value { get; set; }
    
    public AnswerBoolean() : base(AnswerTypeEnum.Boolean) { }
}

public class AnswerDateTime : AnswerBase
{
    public DateTime? Value { get; set; }
    
    public AnswerDateTime() : base(AnswerTypeEnum.DateTime) { }
}

public class AnswerMulti : AnswerBase
{
    public ICollection<QuestionOption> SelectedOptions { get; set; } = new List<QuestionOption>();

    public AnswerMulti() : base(AnswerTypeEnum.MultiValue) { }
}

public class AnswerSingle : AnswerBase
{
    public QuestionOption SelectedOption { get; set; }
    
    public AnswerSingle() : base(AnswerTypeEnum.SingleValue) { }
}