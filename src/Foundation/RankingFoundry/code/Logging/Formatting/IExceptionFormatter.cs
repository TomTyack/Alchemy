using System;
using System.Text;

namespace Sitecore.Foundation.RankingFoundry.Logging.Formatting
{
	public interface IExceptionFormatter
	{
		bool Format(Exception exception, StringBuilder result, bool asHtml);
	}
}
