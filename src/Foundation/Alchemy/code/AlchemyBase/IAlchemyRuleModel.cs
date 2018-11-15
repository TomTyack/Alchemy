using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sitecore.Foundation.AlchemyBase
{
	public interface IAlchemyRuleModel
    {
        string Name { get; set; }

	    [JsonConverter(typeof(StringEnumConverter))]
		Status Status { get; set; }

	    [JsonConverter(typeof(StringEnumConverter))]
		CompletionStatus CompletionStatus { get; set; }

		int Score { get; set; }

	    [JsonConverter(typeof(StringEnumConverter))]
		Importance Importance { get; set; }

	    [JsonConverter(typeof(StringEnumConverter))]
		ConfigurationRole ConfigurationRole { get; set; }

		string Site { get; set; } //The site that this rule applies to if 

		// Rule References
	    [JsonConverter(typeof(StringEnumConverter))]
		RuleDocumentation DocumentationType { get; set; }
		string DocumentationLink { get; set; }

		string DefaultFailureMessage { get; set; }
		string DefaultErrorMessage { get; set; }

		// Special Case Variables
		bool IsProductionCDServer { get; set; }	// Defaults to false, only use if configuration roles are not setup and the rule is to be run on the CD server.
	}
}