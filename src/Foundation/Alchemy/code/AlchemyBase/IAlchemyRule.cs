using System.Collections.Generic;

namespace Sitecore.Foundation.AlchemyBase
{
	public interface IAlchemyRule
	{
		Status Status { get; set; }

		int Score { get; set; }

		Importance Importance { get; set; }

		ConfigurationRole ConfigurationRole { get; set; }

		string Site { get; set; } //The site that this rule applies to if 

		// Rule References
		RuleDocumentation DocumentationType { get; set; }
		string DocumentationLink { get; set; }

		string ErrorMessage { get; set; }
		string FailureReason { get; set; }

		void Run();
	}
}