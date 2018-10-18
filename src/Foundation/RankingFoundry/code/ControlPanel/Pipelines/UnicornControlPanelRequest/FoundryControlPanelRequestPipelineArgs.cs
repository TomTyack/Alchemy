using System.Web;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Security;
using Sitecore.Pipelines;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public class FoundryControlPanelRequestPipelineArgs : PipelineArgs
	{
		public string Verb { get; private set; }

		public HttpContextBase Context { get; private set; }

		public SecurityState SecurityState { get; private set; }

		public IResponse Response { get; set; }

		public FoundryControlPanelRequestPipelineArgs(string verb, HttpContextBase context, SecurityState securityState)
		{
			Verb = verb;
			Context = context;
			SecurityState = securityState;
		}
	}
}
