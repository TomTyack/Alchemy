using System.Web.UI;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Controls
{
	internal class HtmlHeadAndStyles : IControlPanelControl
	{
		public void Render(HtmlTextWriter writer)
		{
			writer.Write(@"<!DOCTYPE html>
			<html>
			<head>
				<title>Alchemy Control Panel</title>
			</head>
			<body>");
		}
	}
}
