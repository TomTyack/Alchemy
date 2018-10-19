using System.Linq;
using System.Web.UI;
using Sitecore.Foundation.RankingFoundry.Configuration.Dependencies;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Controls
{
	/// <summary>
	///	 Renders the current dependency/provider configuration for Unicorn, using IDocumentable to show additional details
	///	 when available.
	/// </summary>
	internal class ConfigurationDetails : IControlPanelControl
	{
		public ConfigurationDetails()
		{
		}

		public string ConfigurationName { get; set; }
		public string ModalId { get; set; }

		public void Render(HtmlTextWriter writer)
		{
			bool collapse = !string.IsNullOrWhiteSpace(ModalId);

			if (collapse) writer.Write(@"
				<div id=""{0}"" class=""overlay"">", ModalId);

			writer.Write(@"
					<article class=""modal"">");

			if (collapse)
				writer.Write(@"
						<h2>{0} Details</h2>", ConfigurationName);

			//RenderType(collapse,
			//	"Target Data Store",
			//	"Defines how items are serialized, for example to disk using YAML format.",
			//	_serializationStore,
			//	writer);

			//RenderType(collapse,
			//	"Source Data Store",
			//	"Defines how source data is read to compare with serialized data. Normally this is a Sitecore data store.",
			//	_sourceDataStore,
			//	writer);

			//RenderType(collapse,
			//	"Evaluator",
			//	"The evaluator decides what to do when included items need updating, creation, or deletion.",
			//	_evaluator,
			//	writer);

			writer.Write(@"
					</article>");

			if (collapse) writer.Write(@"
				</div>");
		}

		

		
	}
}