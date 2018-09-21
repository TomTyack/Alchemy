using System;
using Sitecore.Pipelines;

namespace Sitecore.Foundation.RankingFoundry.Configuration
{
	public class PipelineBasedVariablesReplacer : IContainerDefinitionVariablesReplacer
	{
		public virtual void ReplaceVariables(ContainerDefinition definition)
		{
			if (definition.Name == null) throw new ArgumentException("Configuration without a name was used. Add a name attribute to all configurations.", nameof(definition));

			var args = new UnicornExpandConfigurationVariablesPipelineArgs(definition);

			CorePipeline.Run("unicornExpandConfigurationVariables", args);
		}
	}
}
