using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Foundation.AlchemyBase
{
	public abstract class AlchemyRule : IAlchemyRule
	{
		public AlchemyRule()
		{
			Status = Status.Waiting;  
			if(RuleDependencies == null)
				RuleDependencies = new List<IAlchemyRule>();
		}


	    public string Name { get; set; }
	    public Guid Id { get; set; }

        public string ErrorMessage { get; set; }
		public string FailureReason { get; set; }
		public string DefaultFailureMessage { get; set; }
		public string DefaultErrorMessage { get; set; }

		public List<IAlchemyRule> RuleDependencies { get; set; }

		public Status Status { get; set; }
		public CompletionStatus CompletionStatus { get; set; }

		public int Score { get; set; }
		public Importance Importance { get; set; }

		public ConfigurationRole ConfigurationRole { get; set; }
		public string Site { get; set; }

		public RuleDocumentation DocumentationType { get; set; }
	    public string DocumentationLink { get; set; }

        public bool IsProductionCDServer { get; set; }

	    public abstract void Run();

	    public void Inject(RuleDefinition definition)
	    {
	        this.Id = Guid.NewGuid();
	        this.Name = definition.Name;
	        this.CompletionStatus = definition.CompletionStatus;
	        this.ConfigurationRole = definition.ConfigurationRole;
	        this.Site = definition.Site;

            this.DefaultErrorMessage = definition.DefaultErrorMessage;
	        this.DefaultFailureMessage = definition.DefaultFailureMessage;

	        this.DocumentationLink = definition.DocumentationLink;
	        this.DocumentationType = definition.DocumentationType;
	        //this.ErrorMessage = definition.ErrorMessage;
	        //this.FailureReason = definition.FailureReason;
	        this.Importance = definition.Importance;
	        this.IsProductionCDServer = definition.IsProductionCDServer;
	        this.Score = definition.Score;
	        
        }

        public bool CheckDependencies()
		{
			if (RuleDependencies.Any(x => x.Status != Status.Completed))
			{
				return false;
			}

			return true;
		}

		public bool IsClientFacingProductionServer()
		{
			return ConfigurationRole == ConfigurationRole.ContentDelivery || IsProductionCDServer;
		}

		protected void Errored()
		{
			CompletionStatus = CompletionStatus.Error;
			ErrorMessage = DefaultErrorMessage;
		}

		protected void Failed()
		{
			CompletionStatus = CompletionStatus.Fail;
			this.FailureReason = string.Format(DefaultFailureMessage, Site);
		}
	}
}