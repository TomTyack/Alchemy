using System.Web;
using Sitecore.Foundation.Alchemy.ControlPanel.Responses;
using Sitecore.Foundation.Alchemy.ControlPanel.Security;
using Sitecore.Pipelines;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Pipelines.ControlPanelRequest
{
	public class ControlPanelRequestPipelineArgs : PipelineArgs
	{
		public string Verb { get; private set; }

		public HttpContextBase Context { get; private set; }

		public SecurityState SecurityState { get; private set; }

		public IResponse Response { get; set; }

		public ControlPanelRequestPipelineArgs(string verb, HttpContextBase context, SecurityState securityState)
		{
			Verb = verb;
			Context = context;
			SecurityState = securityState;
		}
	}
}
