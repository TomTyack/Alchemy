using Sitecore.Configuration;
using Sitecore.Foundation.Alchemy.ControlPanel.Security;
using Sitecore.Foundation.Alchemy.Repository;

namespace Sitecore.Foundation.Alchemy.Configuration
{
	/// <summary>
	/// This is the primary class to read configurations with. It reads the configuration provider from Unicorn.config and loads its configurations per its implementation.
	/// </summary>
	public static class AlchemyConfigurationManager
	{
		private static readonly IConfigurationProvider Instance;

		static AlchemyConfigurationManager()
		{
			Instance = (IConfigurationProvider) Factory.CreateObject("/sitecore/alchemy/configurationProvider", true);
			AuthenticationProvider = (IAlchemyAuthenticationProvider)Factory.CreateObject("/sitecore/alchemy/authenticationProvider", false);
			AlchemyRuleRepository = (IAlchemyRuleRepository)Factory.CreateObject("/sitecore/alchemy/alchemyRuleRepository", true);
		}

		public static IConfiguration[] Configurations => Instance.Configurations;

		public static IAlchemyAuthenticationProvider AuthenticationProvider { get; }

		public static IAlchemyRuleRepository AlchemyRuleRepository { get; }
	}
}
