using System;
using System.Web;
using System.Web.UI;
using Sitecore.Foundation.RankingFoundry.Configuration;
using Sitecore.Foundation.RankingFoundry.Configuration.Dependencies;
using Sitecore.StringExtensions;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Controls
{
	internal class ConfigurationInfo : IControlPanelControl
	{
		private readonly IConfiguration _configuration;

		public bool MultipleConfigurationsExist { get; set; }

		public ConfigurationInfo(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Render(HtmlTextWriter writer)
		{
			var configurationHasAnySerializedItems = ControlPanelUtility.HasAnySerializedItems(_configuration);
			var dependents = _configuration.Resolve<ConfigurationDependencyResolver>().Dependencies;

			var modalId = "m" + Guid.NewGuid();

			writer.Write(@"
			<tr>
				<td{0}>", configurationHasAnySerializedItems ? string.Empty : " colspan=\"2\"");

			if (!string.IsNullOrWhiteSpace(_configuration.Description))
				writer.Write(@"
					<p>{0}</p>", _configuration.Description);

			if (configurationHasAnySerializedItems)
			{
				writer.Write(@"
					<span class=""badge""><a href=""#"" data-modal=""{0}"" class=""info"">Show Config</a></span>", modalId);
			}

			var configDetails = _configuration.Resolve<ConfigurationDetails>();
			configDetails.ConfigurationName = _configuration.Name;

			if (!configurationHasAnySerializedItems)
			{
				configDetails.Render(writer);
				new InitialSetup(_configuration).Render(writer);
			}
			else
			{
				configDetails.ModalId = modalId;
				configDetails.Render(writer);

				writer.Write(@"
				</td>
				<td class=""controls"">");

				var htmlConfigName = HttpUtility.UrlEncode(_configuration.Name ?? string.Empty);

				writer.Write(@"
					<a class=""button"" data-basehref=""?verb=Sync&amp;configuration={0}"" href=""?verb=Sync&amp;configuration={0}"">Sync</a>", htmlConfigName);
			}

			writer.Write(@"
				</td>");

			writer.Write(@"
			</tr>");
		}
	}
}