using System;

namespace Sitecore.Foundation.AlchemyBase
{
    public interface IAlchemyRuleIdentifier
    {
	    string UniqueId { get; set; }
        Guid Id { get; set; }
        string Name { get; set; }
    }
}