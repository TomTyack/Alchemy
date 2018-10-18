﻿using System.Web.UI;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Headings;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Controls
{
	internal class BatchProcessingControls : IControlPanelControl
	{
		public void Render(HtmlTextWriter writer)
		{
			writer.Write($@"<h2 class=""syncall""><a data-basehref=""?verb=Sync"" href=""?verb=Sync"">{new HeadingService().GetAllTheThings()} Sync all the things!</a></h2>");

			writer.Write(@"
						<article class=""batch"">
							<section>
								<h4>Selected</h4>

								<ul class=""batch-configurations""></ul>
		
								<p>	
									<a class=""button batch-sync"" href=""#"">Sync Selected</a>
								</p>
								<p>
									<a class=""button batch-reserialize"" href=""#"" onclick=""return confirm(&#39;DANGER: If any of these configurations use Transparent Sync, the items may not exist in the database and reserialize will reset to the database state! Continue?&#39;)"">Reserialize Selected</a>
								</p>
							</section>
						</article>");
		}
	}
}
