using Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Pipelines.UnicornControlPanelRequest
{
	public class VersionVerb : UnicornControlPanelRequestPipelineProcessor
	{
		public VersionVerb() : base("Version")
		{
		}

		protected override IResponse CreateResponse(UnicornControlPanelRequestPipelineArgs args)
		{
			return new PlainTextResponse(UnicornVersion.Current);
		}
	}
}
