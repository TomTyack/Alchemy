using System.Collections.Generic;
using System.Linq;
using Sitecore.Foundation.Alchemy.Configuration;
using Sitecore.Foundation.Alchemy.Configuration.Dependencies;
using Sitecore.Foundation.Alchemy.ControlPanel.Controls;
using Sitecore.Foundation.Alchemy.ControlPanel.Pipelines.ControlPanelRequest;
using Sitecore.Foundation.Alchemy.ControlPanel.Responses;
using Sitecore.StringExtensions;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public class RenderControlPanel : ControlPanelRequestPipelineProcessor
	{
		public RenderControlPanel() : base(string.Empty)
		{
		}

		protected override IResponse CreateResponse(ControlPanelRequestPipelineArgs args)
		{
			return new ControlPanelPageResponse(args.SecurityState, CreateBodyControls(args).ToArray());
		}

		protected virtual IEnumerable<IControlPanelControl> CreateBodyControls(ControlPanelRequestPipelineArgs args)
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
