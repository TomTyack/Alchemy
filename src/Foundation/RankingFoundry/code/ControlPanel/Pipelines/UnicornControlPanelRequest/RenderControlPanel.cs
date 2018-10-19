using System.Collections.Generic;
using System.Linq;
using Sitecore.Foundation.RankingFoundry.Configuration;
using Sitecore.Foundation.RankingFoundry.Configuration.Dependencies;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Controls;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;
using Sitecore.StringExtensions;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public class RenderControlPanel : FoundryControlPanelRequestPipelineProcessor
	{
		public RenderControlPanel() : base(string.Empty)
		{
		}

		protected override IResponse CreateResponse(FoundryControlPanelRequestPipelineArgs args)
		{
			return new ControlPanelPageResponse(args.SecurityState, CreateBodyControls(args).ToArray());
		}

		protected virtual IEnumerable<IControlPanelControl> CreateBodyControls(FoundryControlPanelRequestPipelineArgs args)
		{
			var isAuthorized = args.SecurityState.IsAllowed;

			if (isAuthorized)
			{
				yield return new ReactBootstrap();
			}
			else
			{
				yield return new AccessDenied();
			}
		}
	}
}
