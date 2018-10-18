using Sitecore.Configuration;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Security;

namespace Sitecore.Foundation.RankingFoundry.Configuration
{
	/// <summary>
	/// This is the primary class to read configurations with. It reads the configuration provider from Unicorn.config and loads its configurations per its implementation.
	/// </summary>
	public static class FoundryConfigurationManager
	{
		private static readonly IConfigurationProvider Instance;

		static FoundryConfigurationManager()
		{
			Instance = (IConfigurationProvider) Factory.CreateObject("/sitecore/foundry/configurationProvider", true);
			AuthenticationProvider = (IUnicornAuthenticationProvider)Factory.CreateObject("/sitecore/foundry/authenticationProvider", false);
		}

		public static IConfiguration[] Configurations => Instance.Configurations;
	
		public static IUnicornAuthenticationProvider AuthenticationProvider { get; }
	}
}
