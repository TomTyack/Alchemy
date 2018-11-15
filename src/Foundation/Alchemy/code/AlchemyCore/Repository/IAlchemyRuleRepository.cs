using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Sitecore.Foundation.Alchemy.Repository
{
    public interface IAlchemyRuleRepository
    {
	    int GetRulesCount();
	    IList GetRulesList();
    }
}