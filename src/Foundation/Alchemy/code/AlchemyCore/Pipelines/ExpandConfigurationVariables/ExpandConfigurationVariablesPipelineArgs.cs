using Configy.Parsing;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace Sitecore.Foundation.Alchemy.Pipelines.ExpandConfigurationVariables
{
	public class ExpandConfigurationVariablesPipelineArgs : PipelineArgs
	{
		public ExpandConfigurationVariablesPipelineArgs(ContainerDefinition configuration)
		{
			Assert.ArgumentNotNull(configuration, "configuration");

			Configuration = configuration;
		}

		public ContainerDefinition Configuration { get; }
	}
}
