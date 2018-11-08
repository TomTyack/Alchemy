using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.Foundation.AlchemyBase;

namespace Sitecore.Foundation.AlchemyRules.Cache
{
    public class IsCacheEnabled	: AlchemyRule, IAlchemyRule
    {
	    private const string ContextSiteNotSetError = "Context Site not set";
	    private const string FailureReasonMessage = "cacheHtml attribute is not true for site {0}";

		public override void Run()
	    {
			var siteInfoList = Sitecore.Configuration.Factory.GetSiteInfoList();

		    if (siteInfoList.Any(x => x.Name == Site))
		    {
			    var site = siteInfoList.FirstOrDefault(x => x.Name == Site);

				// Cache enabled when configuration roles not used, or on CD environments
			    bool cacheEnabledAllSites = site.CacheHtml && (ConfigurationRole == ConfigurationRole.All || ConfigurationRole == ConfigurationRole.ContentDelivery);

				// We aren't concerned if cache is not enabled on non CD environments
			    bool cacheEnabledNonCd = !site.CacheHtml && ConfigurationRole != ConfigurationRole.ContentDelivery;

				if (cacheEnabledAllSites || cacheEnabledNonCd)
			    {
					Status = Status.Pass;
			    }
				else
				{
					Status = Status.Fail;
					this.FailureReason = string.Format(FailureReasonMessage, Site);
				}
		    }
		    else
		    {
			    Status = Status.Error;
			    ErrorMessage = ContextSiteNotSetError;
		    } 
		} 
    }
}
