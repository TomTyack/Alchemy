namespace Sitecore.Foundation.AlchemyBase
{
	public abstract class AlchemyRule : IAlchemyRule
	{
		public AlchemyRule()
		{
			Status = Status.Waiting;
			ConfigurationRole = ConfigurationRole.All;
			Site = "website";
		}


		public string ErrorMessage { get; set; }
		public string FailureReason { get; set; }
		public bool Pass { get; set; }
		public Status Status { get; set; }
		public int Score { get; set; }
		public Importance Importance { get; set; }
		public ConfigurationRole ConfigurationRole { get; set; }
		public string Site { get; set; }
		public RuleDocumentation DocumentationType { get; set; }
		public string DocumentationLink { get; set; }

		public abstract void Run();
	}
}