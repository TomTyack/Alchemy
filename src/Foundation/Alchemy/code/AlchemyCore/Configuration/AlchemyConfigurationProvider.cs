﻿using System;
using System.Collections.Generic;	
using System.Linq;
using System.Xml;
using Configy;
using Configy.Containers;
using Configy.Parsing;
using Sitecore.Configuration;     
using Sitecore.Diagnostics;    
using Sitecore.DynamicSerialization;
using Sitecore.Foundation.Alchemy.Engine;
using Sitecore.Foundation.AlchemyBase;

namespace Sitecore.Foundation.Alchemy.Configuration
{
    /// <summary>
    /// Reads Alchemy dependency configurations from XML (e.g. Sitecore web.config section sitecore/alchemy)
    /// </summary>
    public class AlchemyConfigurationProvider : XmlContainerBuilder, IConfigurationProvider
    {
        private IConfiguration[] _configurations;

        public AlchemyConfigurationProvider() : base(null)
        {
        }

        //protected AlchemyConfigurationProvider(IContainerDefinitionVariablesReplacer variablesReplacer) : base(variablesReplacer)
        //{

        //}

        public IConfiguration[] Configurations
        {
            get
            {
                if (_configurations == null)
                    LoadConfigurations();
                return _configurations;
            }
        }


        protected virtual XmlNode GetConfigurationNode()
        {
            return Factory.GetConfigNode("/sitecore/alchemy");
        }

        protected virtual void LoadConfigurations()
        {
            var configNode = GetConfigurationNode();

            Assert.IsNotNull(configNode, "Root Alchemy config node not found. Missing Alchemy.config?");

            var defaultsNode = configNode["defaults"];

            Assert.IsNotNull(defaultsNode, "Alchemy <defaults> node not found. It should be under <Alchemy> config section.");

            var ruleNodes = configNode.SelectNodes("./configurations/configuration");

            // no configs let's get outta here
            if (ruleNodes == null || ruleNodes.Count == 0)
            {
                _configurations = new IConfiguration[0];
                return;
            }

            var parser = new XmlContainerParser(configNode["configurations"], configNode["defaults"], new XmlInheritanceEngine());

            var definitions = parser.GetContainers();

            var configurations = GetContainers(definitions).ToArray();

            foreach (var configuration in configurations)
            {
                // Assert that expected dependencies exist - and in the case of data stores are specifically singletons (WEIRD things happen otherwise)
                //configuration.AssertSingleton(typeof(IDefaultAlchmeyRuleEngine));
                //configuration.AssertSingleton(typeof(ITargetDataStore));
                //configuration.Assert(typeof(IEvaluator));
                //configuration.Assert(typeof(IPredicate));
                //configuration.Assert(typeof(ILogger));
                //configuration.Assert(typeof(ISerializationLoaderLogger));
                //configuration.Assert(typeof(IConsistencyChecker));
                //configuration.Assert(typeof(IDeserializeFailureRetryer));

                // register the configuration with itself. how meta!
                configuration.Register(typeof(IConfiguration), () => new ReadOnlyConfiguration((IConfiguration)configuration), true);
            }

            _configurations = configurations
                .Cast<IConfiguration>()
                .Select(config => (IConfiguration)new ReadOnlyConfiguration(config))
                .ToArray();
        }

        protected override IContainer CreateContainer(ContainerDefinition definition)
        {
            var description = GetAttributeValue(definition.Definition, "description");
			return new MicroConfiguration(definition.Name, description, definition.Extends);
        }

        protected override void RegisterConfigTypeInterface(IContainer container, Type interfaceType, TypeRegistration implementationRegistration, KeyValuePair<string, object>[] unmappedAttributes, XmlElement dependency)
        {
			base.RegisterConfigTypeInterface(container, interfaceType, implementationRegistration, unmappedAttributes, dependency);
			return;

			// IDataStore registrations get special treatment. The implementation must be disambiguated into Source and Target data stores, 
			// which we do by wrapping it in a ConfigurationDataStore factory and manually registering the apropos interface.
			//if ("sourceDataStore".Equals(dependency.Name, StringComparison.OrdinalIgnoreCase))
			//{
			//    Func<object> wrapperFactory = () => new ConfigurationDataStore(new Lazy<IDataStore>(() => (IDataStore)container.Activate(implementationRegistration.Type, unmappedAttributes)));

			//    container.Register(typeof(ISourceDataStore), wrapperFactory, implementationRegistration.SingleInstance);

			//    return;
			//}

			//if ("targetDataStore".Equals(dependency.Name, StringComparison.OrdinalIgnoreCase))
			//{
			//    Func<object> wrapperFactory = () => new ConfigurationDataStore(new Lazy<IDataStore>(() => (IDataStore)container.Activate(implementationRegistration.Type, unmappedAttributes)));

			//    container.Register(typeof(ITargetDataStore), wrapperFactory, implementationRegistration.SingleInstance);
			//}
		}
    }
}
