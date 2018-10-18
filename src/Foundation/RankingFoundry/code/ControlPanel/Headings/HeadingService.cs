using System;
using System.IO;
using System.Reflection;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Headings
{
	public class HeadingService
	{
		static readonly Random Random = new Random();

		private static readonly string[] HtmlChoices = {
			"Sitecore.Foundation.RankingFoundry.ControlPanel.Headings.Unicorn.html",
			"Sitecore.Foundation.RankingFoundry.ControlPanel.Headings.Unicorn.svg.html",
			"Sitecore.Foundation.RankingFoundry.ControlPanel.Headings.Unicorn2.svg.html",
			"Sitecore.Foundation.RankingFoundry.ControlPanel.Headings.Unicorn3.svg.html"
		};

		public string GetHeadingHtml()
		{
			// heh heh :)
			if (DateTime.Today.Month == 4 && DateTime.Today.Day == 1) return ReadResource("Sitecore.Foundation.RankingFoundry.ControlPanel.Headings.April.svg.html");

			var headerIndex = Random.Next(0, HtmlChoices.Length);

			return ReadResource(HtmlChoices[headerIndex]);
		}

		public string GetControlPanelHeadingHtml()
		{
			return ReadResource("Sitecore.Foundation.RankingFoundry.ControlPanel.Headings.ControlPanel.svg.html");
		}

		public string GetAllTheThings()
		{
			// http://nearby-pla.net/images/all-the-things-blank-clean-template.html FTW
			return ReadResource("Sitecore.Foundation.RankingFoundry.ControlPanel.Headings.Allthethings.svg.html");
		}

		protected virtual string ReadResource(string name)
		{
			var assembly = Assembly.GetExecutingAssembly();

			using (Stream stream = assembly.GetManifestResourceStream(name))
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
