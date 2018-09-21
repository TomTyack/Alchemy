﻿using System;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Foundation.RankingFoundry.Configuration;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Headings;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;

// ReSharper disable RedundantArgumentNameForLiteralExpression
// ReSharper disable RedundantArgumentName
// ReSharper disable UnusedMember.Global

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Pipelines.UnicornControlPanelRequest
{
	public class SyncVerb : UnicornControlPanelRequestPipelineProcessor
	{
		private readonly SerializationHelper _helper;

		public SyncVerb() : this("Sync", new SerializationHelper())
		{
		}

		protected SyncVerb(string verb, SerializationHelper helper) : base(verb)
		{
			_helper = helper;
		}

		protected override IResponse CreateResponse(UnicornControlPanelRequestPipelineArgs args)
		{
			return new WebConsoleResponse("Sync Unicorn", args.SecurityState.IsAutomatedTool, new HeadingService(), progress => Process(progress, new WebConsoleLogger(progress, args.Context.Request.QueryString["log"])));
		}

		protected virtual void Process(IProgressStatus progress, ILogger additionalLogger)
		{
			var configurations = ResolveConfigurations();

			_helper.SyncConfigurations(configurations, progress, additionalLogger);
		}

		protected virtual IConfiguration[] ResolveConfigurations()
		{
			// This logic is present in all verbs. Marked for refactoring
			var config = HttpContext.Current.Request.QueryString["configuration"];
			var exclude = HttpContext.Current.Request.QueryString["exclude"];
			var targetConfigurations = ControlPanelUtility.ResolveConfigurationsFromQueryParameter(config, exclude);

			if (targetConfigurations.Length == 0) throw new ArgumentException("Configuration(s) requested were not defined.");

			// skipTransparent does not apply when syncing a single config explicitly
			if (targetConfigurations.Length == 1) return targetConfigurations;

			// optionally skip transparent sync configs when syncing
			var skipTransparent = HttpContext.Current.Request.QueryString["skipTransparentConfigs"] ?? "0";
			if (skipTransparent == "1" || skipTransparent.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
			{
				targetConfigurations = targetConfigurations.SkipTransparentSync().ToArray();

				if (targetConfigurations.Length == 0) Log.Warn("[Unicorn] All configurations were transparent sync and skipTransparentConfigs was active. Syncing nothing.", this);
			}

			return targetConfigurations;
		}
	}
}
