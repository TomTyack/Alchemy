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
			var modalId = "m" + Guid.NewGuid();

			writer.Write(@"
				</td>");

			writer.Write(@"
			</tr>");
		}
	}
}