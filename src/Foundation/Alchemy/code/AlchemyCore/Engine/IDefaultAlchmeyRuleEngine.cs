using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.AlchemyBase;
using Sitecore.Foundation.AlchemyBase.ResponseWrapper;

namespace Sitecore.Foundation.Alchemy.Engine
{
	public interface IDefaultAlchmeyRuleSet
	{
	    string Group { get; set; }
	    void Begin();
	    List<IAlchemyRule> GetRulesList();
	}
}
