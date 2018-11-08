using System.Net;
using System.Web;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Security
{
	public interface IAlchemyAuthenticationProvider
	{
		string GetChallengeToken();
		SecurityState ValidateRequest(HttpRequestBase request);
		WebClient CreateAuthenticatedWebClient(string remoteUnicornUrl);
	}
}
