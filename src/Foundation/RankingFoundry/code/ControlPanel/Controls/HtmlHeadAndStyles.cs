using System.Web.UI;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Controls
{
	internal class HtmlHeadAndStyles : IControlPanelControl
	{
		public void Render(HtmlTextWriter writer)
		{
			writer.Write(@"<!DOCTYPE html>
			<html>
			<head>
				<title>Foundry Control Panel</title>
			</head>
			<body>");
		}
	}
}
