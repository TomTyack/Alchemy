using System;

namespace Sitecore.Foundation.AlchemyBase
{
    public interface IAlchemyRuleIdentifier
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}