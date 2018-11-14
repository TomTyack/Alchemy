namespace Sitecore.Foundation.AlchemyBase
{
    public interface IAlchemyRule : IAlchemyRuleModel
    {
        void Inject(RuleDefinition ruleDefinition);
    }
}