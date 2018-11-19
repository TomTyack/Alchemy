using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Sitecore.Foundation.AlchemyBase;

namespace Sitecore.Foundation.Alchemy.Repository
{
    public interface IAlchemyRuleRepository
    {
	    int GetRulesCount();
	    IList GetRulesList();
	    IAlchemyRule GetRule(string id);
        Task BeginProcessing(string id);
	}
}