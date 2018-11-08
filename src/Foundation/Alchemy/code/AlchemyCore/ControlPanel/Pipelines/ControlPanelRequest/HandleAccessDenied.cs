using System.Net;
using Sitecore.Foundation.Alchemy.ControlPanel.Controls;
using Sitecore.Foundation.Alchemy.ControlPanel.Pipelines.ControlPanelRequest;
using Sitecore.Foundation.Alchemy.ControlPanel.Responses;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public class HandleAccessDenied : ControlPanelRequestPipelineProcessor
	{
		// NOTE: because each processor checks for authentication individually this is more of an unhandled access denied handler as opposed to a gate
		// Should come before control panel in pipeline
		public HandleAccessDenied() : base(string.Empty)
		{
		}

		protected override bool HandlesVerb(ControlPanelRequestPipelineArgs args)
		{
			return !args.SecurityState.IsAllowed;
		}

		protected override IResponse CreateResponse(ControlPanelRequestPipelineArgs args)
		{
			if (args.SecurityState.IsAutomatedTool)
			{
				return new PlainTextResponse("Automated tool authentication failed.", HttpStatusCode.Unauthorized);
			}

			return new ControlPanelPageResponse(args.SecurityState, new AccessDenied());
		}
	}
}
