using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Foundation.Alchemy.Configuration;
using Sitecore.Foundation.Alchemy.Engine;

namespace Sitecore.Foundation.Alchemy.Repository
{
    public class AlchemyRuleRepository : IAlchemyRuleRepository
    {
	    private List<IDefaultAlchmeyRuleSet> _ruleEngines;

	    public AlchemyRuleRepository()
	    {
		    _ruleEngines = new List<IDefaultAlchmeyRuleSet>();
		    foreach (var configuration in AlchemyConfigurationManager.Configurations)
		    {
			    var ruleSet = configuration.Resolve<IDefaultAlchmeyRuleSet>();
			    ruleSet.Group = configuration.Name;
			    _ruleEngines.Add(ruleSet);
		    }
		    //   var array = AlchemyConfigurationManager.Configurations.Select(configuration => configuration.Resolve<IDefaultAlchmeyRuleSet>()).ToArray();
		    //_ruleEngines = array.ToList();
	    }

	    public int GetRulesCount()
	    {
		    return _ruleEngines.Select(x => x.GetRulesList()).Count();
	    }

	    public IList GetRulesList()
	    {
			var responses = _ruleEngines.Select(x => x.GetRulesList().Select(y => new { Id = y.Id, Name = y.Name, Group = x.Group, Status = y.Status.ToString() })).ToList();
		    return responses;
	    }
	}
}