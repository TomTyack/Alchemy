using System.IO;
using System.Text;
using Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Logging;
using Sitecore.StringExtensions;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio
{
	internal static class StreamWriterExtensions
	{
		public static void SendMessage(this StreamWriter writer, ReportType type, MessageLevel level, string message)
		{
			var encodedMessage = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
			var report = "{0}|{1}|{2}".FormatWith(type, level, encodedMessage);
			writer.WriteLine(report);
			writer.Flush();
		}
	}
}
