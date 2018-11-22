using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Foundation.Alchemy.Configuration;
using Sitecore.Foundation.Alchemy.Engine;
using Sitecore.Foundation.AlchemyBase;

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
			var responses = _ruleEngines.Select(x =>
					x.GetRulesList().Select(y => new { UniqueId = y.UniqueId, Id = y.Id, Name = y.Name, Group = x.Group, Status = y.Status.ToString()}))
				.ToList();									 
			return responses;
		}

		public IAlchemyRule GetRule(string id)
		{
			return _ruleEngines.SelectMany(x => x.GetRulesList()).FirstOrDefault(x => x.UniqueId.ToLower() == id.ToLower());
		}

		public async Task BeginProcessing(string id)
		{
			var rule = GetRule(id);
			if (rule.Status == Status.Waiting)
				await rule.RunAsync();
		}
	}
}