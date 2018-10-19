using System.Net;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Controls;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public class HandleAccessDenied : FoundryControlPanelRequestPipelineProcessor
	{
		// NOTE: because each processor checks for authentication individually this is more of an unhandled access denied handler as opposed to a gate
		// Should come before control panel in pipeline
		public HandleAccessDenied() : base(string.Empty)
		{
		}

		protected override bool HandlesVerb(FoundryControlPanelRequestPipelineArgs args)
		{
			return !args.SecurityState.IsAllowed;
		}

		protected override IResponse CreateResponse(FoundryControlPanelRequestPipelineArgs args)
		{
			if (args.SecurityState.IsAutomatedTool)
			{
				return new PlainTextResponse("Automated tool authentication failed.", HttpStatusCode.Unauthorized);
			}

			return new ControlPanelPageResponse(args.SecurityState, new AccessDenied());
		}
	}
}
