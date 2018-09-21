using Sitecore.Foundation.RankingFoundry.Configuration;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public class ChallengeVerb : UnicornControlPanelRequestPipelineProcessor
	{
		public ChallengeVerb() : base("Challenge", requireAuthentication: false)
		{
		}

		protected override IResponse CreateResponse(UnicornControlPanelRequestPipelineArgs args)
		{
			return new PlainTextResponse(UnicornConfigurationManager.AuthenticationProvider.GetChallengeToken());
		}
	}
}
