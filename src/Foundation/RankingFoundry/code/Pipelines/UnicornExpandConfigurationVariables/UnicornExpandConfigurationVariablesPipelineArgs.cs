﻿using Configy.Parsing;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace Sitecore.Foundation.RankingFoundry.Pipelines.UnicornExpandConfigurationVariables
{
	public class UnicornExpandConfigurationVariablesPipelineArgs : PipelineArgs
	{
		public UnicornExpandConfigurationVariablesPipelineArgs(ContainerDefinition configuration)
		{
			Assert.ArgumentNotNull(configuration, "configuration");

			Configuration = configuration;
		}

		public ContainerDefinition Configuration { get; }
	}
}
