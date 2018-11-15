namespace Sitecore.Foundation.AlchemyBase
{
    public interface IAlchemyRule : IAlchemyRuleModel
    {
        string ErrorMessage { get; set; }
        string FailureReason { get; set; }

        void Inject(RuleDefinition ruleDefinition);
    }
}