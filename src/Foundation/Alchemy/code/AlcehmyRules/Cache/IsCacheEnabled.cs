using System;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Diagnostics;
using Sitecore.Foundation.AlchemyBase;       

namespace Sitecore.Foundation.AlchemyRules.Cache
{
    public class IsCacheEnabled	: AlchemyRule, IAlchemyRule
    {
	    public override void Run()
	    {
			var siteInfoList = Configuration.Factory.GetSiteInfoList();

		    if (siteInfoList.Any(x => x.Name.ToLower() == Site.ToLower()))
		    {
			    var site = siteInfoList.FirstOrDefault(x => x.Name.ToLower() == Site.ToLower());

			    // Cache enabled when configuration roles not used, or on CD environments
			    bool cacheEnabledAllSites = site.CacheHtml && (ConfigurationRole == ConfigurationRole.All || ConfigurationRole == ConfigurationRole.ContentDelivery);

			    // We aren't concerned if cache is not enabled on non CD environments
			    bool cacheEnabledNonCd = !site.CacheHtml && ConfigurationRole != ConfigurationRole.ContentDelivery;

			    if (cacheEnabledAllSites || cacheEnabledNonCd)
			    {
				    CompletionStatus = CompletionStatus.Pass;
			    }
			    else
			    {
				    Failed();
			    }
		        Status = Status.Completed;
            }
		    else
		    {
			    Errored();
		    }
		} 
    }
}
