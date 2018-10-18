using Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;
using Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Responses;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Pipelines.UnicornControlPanelRequest
{
	// ReSharper disable once InconsistentNaming
	public class VSSyncVerb : SyncVerb
	{
		public VSSyncVerb() : base("VSSync", new SerializationHelper())
		{
		}

		protected override IResponse CreateResponse(UnicornControlPanelRequestPipelineArgs args)
		{
			return new StreamingEncodedLogResponse(Process);
		}
	}
}
