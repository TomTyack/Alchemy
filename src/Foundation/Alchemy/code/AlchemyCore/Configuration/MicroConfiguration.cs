using System.Collections.Generic;
using Configy.Containers;
using Sitecore.Foundation.AlchemyBase;

namespace Sitecore.Foundation.Alchemy.Configuration
{
	public class MicroConfiguration : MicroContainer, IConfiguration
	{
		public MicroConfiguration(string name, string description, string extends) : base(name, extends)
		{
			Description = description;
			//AlchemyRules = rules;
			//Dependencies = dependencies ?? new string[0];
			//IgnoredImplicitDependencies = ignoredDependencies ?? new string[0];
		}

		public List<IAlchemyRule> AlchemyRules { get; set; }
		public string Description { get; }
		public string[] Dependencies { get; set; }
		public string[] IgnoredImplicitDependencies { get; set; }
	}
}
