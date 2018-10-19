using System.Web.UI;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Controls
{
    /// <summary>
    /// ReactBootstrap - Adds the HTML placeholder onto the page.
    /// </summary>
    internal class ReactBootstrap : IControlPanelControl
	{
		public void Render(HtmlTextWriter writer)
		{
			writer.Write(@"<article>");
			writer.Write(@"<h2>Alchemy</h2>");

			writer.Write("<section>");
			writer.Write("<h1>Loading</h1>");
			writer.Write("</section>");

		    writer.Write("<div id=\"app\">");
		    writer.Write("</div>");

            writer.Write(@"</article>");
		}
	}
}
