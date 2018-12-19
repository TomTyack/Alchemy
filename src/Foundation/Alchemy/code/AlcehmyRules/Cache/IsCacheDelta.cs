using System;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Diagnostics;
using Sitecore.Foundation.AlchemyBase;       

namespace Sitecore.Foundation.AlchemyRules.Cache
{
    public class IsCacheDelta : AlchemyRule, IAlchemyRule
    {
	    public override void Run()
	    {
			Failed();
		} 
    }
}
