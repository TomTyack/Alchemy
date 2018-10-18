﻿using System.Web;
using System.Web.UI;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Controls
{
	internal class AccessDenied : IControlPanelControl
	{
		public void Render(HtmlTextWriter writer)
		{
			writer.Write("<h4>Access Denied</h4>");
			writer.WriteLine("<!--");
			writer.WriteLine("                       (");
			writer.WriteLine("            _           ) )");
			writer.WriteLine("         _,(_)._        ((");
			writer.WriteLine("    ___,(_______).        )");
			writer.WriteLine("  ,\'__.   /       \\    /\\_");
			writer.WriteLine(" /,\' /  |\"\"|       \\  /  /");
			writer.WriteLine("| | |   |__|       |,\'  /");
			writer.WriteLine(" \\`.|                  /");
			writer.WriteLine("  `. :           :    /");
			writer.WriteLine("    `.            :.,\'");
			writer.WriteLine("      `-.________,-\'");
			writer.WriteLine("-->");
			writer.Write("<p>You need to <a href=\"/sitecore/admin/login.aspx?ReturnUrl={0}\">sign in to Sitecore as an administrator</a> to use the Unicorn control panel.</p>", HttpUtility.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery));

			HttpContext.Current.Response.TrySkipIisCustomErrors = true;

			// Returning 401 is causing issues on Sitecore 9. See #287 https://github.com/SitecoreUnicorn/Unicorn/issues/287
			// Returning 418 I'm a Teapot, instead. Unicorn refuses to brew coffee to people not authenticated.
			// Assuming it is doubtful Sitecore has, or will ever, do any special handling for this situation.
			HttpContext.Current.Response.StatusCode = 418;
		}
	}
}
