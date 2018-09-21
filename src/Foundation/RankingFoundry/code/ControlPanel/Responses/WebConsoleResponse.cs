﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Timers;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Headings;
using Sitecore.SecurityModel;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.Responses
{
	/// <summary>
	/// Creates a Html5WebConsole to render something into, using Unicorn chrome
	/// 
	/// Note that classes using this need to use LoggingContext/WebConsoleLogger to attach the WebConsole
	/// to the current Unicorn logger instance or they will not receive Unicorn log output.
	/// </summary>
	public class WebConsoleResponse : IResponse
	{
		private readonly string _title;
		private readonly Action<IProgressStatus> _processAction;
		private readonly bool _isAutomatedTool;
		private readonly HeadingService _headingService;

		public WebConsoleResponse(string title, bool isAutomatedTool, HeadingService headingService, Action<IProgressStatus> processAction)
		{
			Assert.ArgumentNotNullOrEmpty(title, "title");
			Assert.ArgumentNotNull(processAction, "processAction");
			Assert.ArgumentNotNull(headingService, "headingService");

			_title = title;
			_processAction = processAction;
			_isAutomatedTool = isAutomatedTool;
			_headingService = headingService;
		}

		public virtual void Execute(HttpResponseBase response)
		{
			if (_isAutomatedTool)
			{
				var console = new TextWebConsole(response);

				console.WriteLine(_title + "\n\n");

				try
				{
					console.Render(ProcessInternal);
				}
				catch(Exception ex)
				{
					// writing the exception will also trigger HasErrors
					console.WriteException(ex);
				}

				if (console.HasErrors)
				{
					// we cannot return HTTP 500 because headers will have already been sent by now
					response.Write("****ERROR OCCURRED****");
				}

				response.End();
			}
			else
			{
				var console = new CustomStyledHtml5WebConsole(response);
				console.Title = _title;
				console.Render(ProcessInternal);
			}
		}

		protected virtual void ProcessInternal(IProgressStatus progress)
		{
			if (_headingService != null && !_isAutomatedTool)
			{
				progress.ReportStatus(_headingService.GetHeadingHtml());
			}

			// note: these logs are intentionally to progress and not loggingConsole as we don't need them in the Sitecore logs

			progress.ReportTransientStatus("Executing.");

			var heartbeat = new Timer(3000);

			var timer = new Stopwatch();
			timer.Start();

			heartbeat.AutoReset = true;
			heartbeat.Elapsed += (sender, args) =>
			{
				var elapsed = Math.Round(timer.ElapsedMilliseconds / 1000d);

				try
				{
					progress.ReportTransientStatus("Executing for {0} sec.", elapsed.ToString(CultureInfo.InvariantCulture));
				}
				catch
				{
					// e.g. HTTP connection disconnected - prevent infinite looping
					heartbeat.Stop();
				}
			};

			heartbeat.Start();

			try
			{
				using (new SecurityDisabler())
				{
					_processAction(progress);
				}
			}
			finally
			{
				heartbeat.Stop();
			}

			timer.Stop();

			progress.Report(100);
			progress.ReportTransientStatus("Operation completed.");
			progress.ReportStatus(_isAutomatedTool ? "\r\n" : "<br>");
			progress.ReportStatus(_isAutomatedTool ? $"Completed in {timer.ElapsedMilliseconds}ms." : $"Operation completed in {timer.ElapsedMilliseconds}ms. Want to <a href=\"?verb=\">return to the control panel?</a>");
		}

		private class CustomStyledHtml5WebConsole : Html5WebConsole
		{
			private readonly HttpResponseBase _response;
			private readonly object _writeLock = new object();

			public CustomStyledHtml5WebConsole(HttpResponseBase response) : base(response)
			{
				_response = response;
			}

			protected override void RenderPageHead()
			{
				_response.Write(new HeadingService().GetControlPanelHeadingHtml());
			}

			public override void WriteScript(string script)
			{
				lock (_writeLock)
				{
					base.WriteScript(script);
				}
			}

			public override void RenderResources()
			{
				base.RenderResources();
				_response.Write("<link href='https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700,400italic' rel='stylesheet' type='text/css'>");
				_response.Write(@"<style>
					.wrapper { width: auto; max-width: 1850px; } 
					a, a:visited { color: lightblue; } 
					#console{ height: 40em; } 
					h1 { font-size: 3em; } 
					h1, p { font-family: 'Source Sans Pro', sans-serif; } 
					.line { margin: 1em 0 0 0; font-size: 1.2em; display: block; }
					.line-inner { display: block; font-size: 0.9em; }
					.line-smaller { font-size: 0.7em !important; margin-bottom: .4em; font-style: italic; color: gray; }
				</style>");
			}
		}
	}
}
