using System.Web;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Responses
{
	public interface IResponse
	{
		void Execute(HttpResponseBase response);
	}
}
