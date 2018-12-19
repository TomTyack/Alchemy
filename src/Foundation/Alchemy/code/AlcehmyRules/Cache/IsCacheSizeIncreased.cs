using System;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Diagnostics;
using Sitecore.Foundation.AlchemyBase;

namespace Sitecore.Foundation.AlchemyRules.Cache
{
	public class IsCacheSizeIncreased : AlchemyRule, IAlchemyRule
	{
		public override void Run()
		{
			var siteInfoList = Sitecore.Configuration.Factory.GetSiteInfoList();
			if (siteInfoList.Any(x => x.Name.ToLower() == Site.ToLower()))
			{
				var site = siteInfoList.FirstOrDefault(x => x.Name.ToLower() == Site.ToLower());

				// Mainly concerned about cache being increased on CD or Production servers if Config roles not enabled.
				if (IsClientFacingProductionServer() && site.HtmlCache.InnerCache.MaxSize > 20000)
				{
					CompletionStatus = CompletionStatus.Pass;
				}
				else
				{
					Failed();
				}
			}
			else
			{
				Errored();
			}

			Status = Status.Completed;
		}
	}
}
