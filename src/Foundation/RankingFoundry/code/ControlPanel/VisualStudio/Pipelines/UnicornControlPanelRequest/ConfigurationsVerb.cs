using System.Linq;
using Sitecore.Foundation.RankingFoundry.Configuration;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Pipelines.UnicornControlPanelRequest
{
	public class ConfigurationsVerb : UnicornControlPanelRequestPipelineProcessor
	{
		public ConfigurationsVerb() : base("Configurations")
		{
		}

		protected override IResponse CreateResponse(UnicornControlPanelRequestPipelineArgs args)
		{
			return new PlainTextResponse(string.Join(",", UnicornConfigurationManager.Configurations.Select(config => config.Name)));
		}
	}
}
