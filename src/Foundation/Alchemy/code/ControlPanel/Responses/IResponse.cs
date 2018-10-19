using System.Web;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Responses
{
	public interface IResponse
	{
		void Execute(HttpResponseBase response);
	}
}
