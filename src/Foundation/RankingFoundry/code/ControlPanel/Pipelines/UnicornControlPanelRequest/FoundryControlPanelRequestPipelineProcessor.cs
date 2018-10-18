using System;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public abstract class FoundryControlPanelRequestPipelineProcessor
	{
		private readonly string _verbHandled;
		private readonly bool _requireAuthentication;
		private readonly bool _abortPipelineIfHandled;

		protected FoundryControlPanelRequestPipelineProcessor(string verbHandled, bool requireAuthentication = true, bool abortPipelineIfHandled = true)
		{
			_verbHandled = verbHandled;
			_requireAuthentication = requireAuthentication;
			_abortPipelineIfHandled = abortPipelineIfHandled;
		}

		public virtual void Process(FoundryControlPanelRequestPipelineArgs args)
		{
			bool handled = HandlesVerb(args);

			if (!handled)
			{
				if (_verbHandled.Equals(args.Verb ?? string.Empty, StringComparison.OrdinalIgnoreCase) && !args.SecurityState.IsAllowed && args.SecurityState.IsAutomatedTool)
				{
					args.Response = new PlainTextResponse("Unable to authorize request, ensure that your shared secrets match.", System.Net.HttpStatusCode.Forbidden);
					args.AbortPipeline();
				}
				return;
			}

			args.Response = CreateResponse(args);

			if(_abortPipelineIfHandled) args.AbortPipeline();
		}

		/// <summary>
		/// Verbs note: passing an empty string to _verbHandled makes you the default page when no verb is passed
		/// Passing null as _verbHandled makes you the catch-all page for every request not handled by previous pipeline handlers
		/// </summary>
		protected virtual bool HandlesVerb(FoundryControlPanelRequestPipelineArgs args)
		{
			if (_requireAuthentication && !args.SecurityState.IsAllowed) return false;

			if (_verbHandled == null) return true;

			return _verbHandled.Equals(args.Verb ?? string.Empty, StringComparison.OrdinalIgnoreCase);
		}

		protected abstract IResponse CreateResponse(FoundryControlPanelRequestPipelineArgs args);
	}
}
