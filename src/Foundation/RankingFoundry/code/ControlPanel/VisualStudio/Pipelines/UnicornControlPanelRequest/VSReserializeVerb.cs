using Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;
using Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Responses;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Pipelines.UnicornControlPanelRequest
{
	// ReSharper disable once InconsistentNaming
	public class VSReserializeVerb : ReserializeVerb
	{
		public VSReserializeVerb() : base("VSReserialize", new SerializationHelper())
		{
		}

		protected override IResponse CreateResponse(UnicornControlPanelRequestPipelineArgs args)
		{
			return new StreamingEncodedLogResponse(Process);
		}
	}
}
