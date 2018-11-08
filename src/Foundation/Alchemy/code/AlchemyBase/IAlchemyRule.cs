using System.Collections.Generic;

namespace Sitecore.Foundation.AlchemyBase
{
	public interface IAlchemyRule
	{
		List<IAlchemyRule> RuleDependencies { get; set; }

		Status Status { get; set; }
		CompletionStatus CompletionStatus { get; set; }

		int Score { get; set; }

		Importance Importance { get; set; }

		ConfigurationRole ConfigurationRole { get; set; }

		string Site { get; set; } //The site that this rule applies to if 

		// Rule References
		RuleDocumentation DocumentationType { get; set; }
		string DocumentationLink { get; set; }

		string ErrorMessage { get; set; }
		string FailureReason { get; set; }

		string DefaultFailureMessage { get; set; }
		string DefaultErrorMessage { get; set; }

		void Run();


		// Special Case Variables

		bool IsProductionCDServer { get; set; }	// Defaults to false, only use if configuration roles are not setup and the rule is to be run on the CD server.
	}
}