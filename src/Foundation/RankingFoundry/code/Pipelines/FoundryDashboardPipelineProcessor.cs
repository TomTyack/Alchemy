using System;
using System.Net;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Foundation.RankingFoundry.Configuration;        
using Sitecore.Pipelines;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.SecurityModel;
using Sitecore.Sites;  
using SecurityState = Sitecore.Foundation.RankingFoundry.ControlPanel.Security.SecurityState;

namespace Sitecore.Foundation.RankingFoundry.Pipelines
{
    public class FoundryDashboardPipelineProcessor : HttpRequestProcessor
    {
        private readonly string _activationUrl;
        private readonly string _activationSite;

        public FoundryDashboardPipelineProcessor(string activationUrl, string activationSite)
        {
            _activationUrl = activationUrl;
            _activationSite = activationSite;
        }

        public override void Process(HttpRequestArgs args)
        {
            if (string.IsNullOrWhiteSpace(_activationUrl)) return;

            if (args.Context.Request.RawUrl.StartsWith(_activationUrl, StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(_activationSite))
                {
                    ProcessRequest(args.Context);
                    args.Context.Response.End();
                }
                else
                {
                    using (new SiteContextSwitcher(Factory.GetSite(_activationSite)))
                    {
                        ProcessRequest(args.Context);
                        args.Context.Response.End();
                    }
                }
            }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            context.Server.ScriptTimeout = 86400;

            // workaround to allow streaming output without an exception in Sitecore 8.1 Update-3 and later
            context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";

            var verb = context.Request.QueryString["verb"];

            var authProvider = FoundryConfigurationManager.AuthenticationProvider;
            SecurityState securityState;
            if (authProvider != null)
            {
                securityState = FoundryConfigurationManager.AuthenticationProvider.ValidateRequest(new HttpRequestWrapper(HttpContext.Current.Request));
            }
            else securityState = new SecurityState(false, false);

            // this securitydisabler allows the control panel to execute unfettered when debug compilation is enabled but you are not signed into Sitecore
            //using (new SecurityDisabler())
            //{
            //    var pipelineArgs = new UnicornControlPanelRequestPipelineArgs(verb, new HttpContextWrapper(context), securityState);

            //    CorePipeline.Run("unicornControlPanelRequest", pipelineArgs, true);

            //    if (pipelineArgs.Response == null)
            //    {
            //        pipelineArgs.Response = new PlainTextResponse("Not Found", HttpStatusCode.NotFound);
            //    }

            //    if (securityState.IsAllowed)
            //    {
            //        context.Response.AddHeader("X-Unicorn-Version", FoundryVersion.Current);
            //    }

            //    pipelineArgs.Response.Execute(new HttpResponseWrapper(context.Response));
            //}
        }
    }
}