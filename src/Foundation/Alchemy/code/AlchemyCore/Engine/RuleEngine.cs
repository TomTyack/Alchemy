using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sitecore.Diagnostics;
using Sitecore.Foundation.Alchemy.Configuration;
using Sitecore.Foundation.AlchemyBase;
using Sitecore.Foundation.AlchemyBase.ResponseWrapper;

namespace Sitecore.Foundation.Alchemy.Engine
{
	public class RuleEngine : IDefaultAlchmeyRuleSet
    {
		protected readonly List<IAlchemyRule> AlchemyRules = new List<IAlchemyRule>();

		public RuleEngine(XmlNode configNode)
		{
			Assert.ArgumentNotNull(configNode, "configNode");
            var rules = configNode.ChildNodes;

            foreach (XmlNode rule in rules)
            {
                if (rule.NodeType == XmlNodeType.Element && rule.Name.Equals("alchemyRule"))
                {
                    var ruleDefinition = new RuleDefinition(rule);
                    var ruleb = XmlActivator.CreateObject<IAlchemyRule>(rule);
                    ruleb.Inject(ruleDefinition);
                    AlchemyRules.Add(ruleb);
                }
            }
        }

        public void Begin()
        {

        }

        public List<IAlchemyRule> GetRulesList()
        {
            return AlchemyRules;
        }
    }
}
