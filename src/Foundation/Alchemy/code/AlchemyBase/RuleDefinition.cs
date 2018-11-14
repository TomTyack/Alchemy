using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sitecore.Foundation.AlchemyBase
{
    public class RuleDefinition : IAlchemyRuleModel
    {
        public RuleDefinition(XmlNode definition)
        {
            this.Definition = definition;
        }

        public XmlNode Definition { get; set; }

        public string Name { get { return GetStringValue("name"); } set {  }}
                                                                   
        public Status Status { get { return ParseEnum<Status>("Status", Status.Waiting); } set {} }
        public CompletionStatus CompletionStatus { get { return ParseEnum<CompletionStatus>("CompletionStatus", CompletionStatus.Pass); } set { } }
        public int Score { get { return Int32.Parse(GetStringValue("Score")); } set { } }
        public Importance Importance { get { return ParseEnum<Importance>("Importance", Importance.Minor); } set { } }
        public ConfigurationRole ConfigurationRole { get { return ParseEnum<ConfigurationRole>("ConfigurationRole", ConfigurationRole.ContentManagement); } set { } }
        public string Site { get { return GetStringValue("Site"); } set { } }
        public RuleDocumentation DocumentationType { get { return ParseEnum<RuleDocumentation>("RuleDocumentation", RuleDocumentation.Other); } set { } }
        public string DocumentationLink { get { return GetStringValue("DocumentationLink"); } set { } }
        public string ErrorMessage { get { return GetStringValue("ErrorMessage"); } set { } }
        public string FailureReason { get { return GetStringValue("FailureReason"); } set { } }
        public string DefaultFailureMessage { get { return GetStringValue("DefaultFailureMessage"); } set { } }
        public string DefaultErrorMessage { get { return GetStringValue("DefaultErrorMessage"); } set { } }

	    public bool IsProductionCDServer
	    {
		    get
		    {
				return GetBoolValue("IsProductionCDServer", true);
		    } set { }
	    }

        public string GetStringValue(string attributeName)
        {
            XmlAttributeCollection attributes = this.Definition.Attributes;
            if (attributes == null)
                return (string)null;
            XmlAttribute xmlAttribute = attributes[attributeName];
            if (xmlAttribute == null)
                return (string)null;
            return xmlAttribute.InnerText;
        }

	    public bool GetBoolValue(string attribute, bool defaultValue = true)
	    {
		    if (string.IsNullOrWhiteSpace(attribute) || string.IsNullOrWhiteSpace(GetStringValue(attribute)))
			    return defaultValue;

		    return Boolean.Parse(GetStringValue(attribute));
	    }

        public T ParseEnum<T>(string attribute, T defaultValue)
        {
	        if (string.IsNullOrWhiteSpace(attribute) || string.IsNullOrWhiteSpace(GetStringValue(attribute)))
		        return defaultValue;

			return (T)Enum.Parse(typeof(T), GetStringValue(attribute), true);
        }
    }
}
