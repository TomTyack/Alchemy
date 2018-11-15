using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using Sitecore.Configuration;
using Sitecore.Foundation.Alchemy.ControlPanel.Controls;
using Sitecore.SecurityModel;
using SecurityState = Sitecore.Foundation.Alchemy.ControlPanel.Security.SecurityState;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Responses
{
	public class ControlPanelPageResponse : IResponse
	{
		private readonly SecurityState _securityState;
		private readonly IControlPanelControl[] _controls;

		public ControlPanelPageResponse(SecurityState securityState, params IControlPanelControl[] controls)
		{
			_securityState = securityState;
			_controls = controls;
		}

		public virtual void Execute(HttpResponseBase response)
		{
			response.StatusCode = 200;
			response.ContentType = "text/html";

		    var masterControls = new List<IControlPanelControl>();

            string content = string.Empty;

			string uiLocationFolder = Settings.GetSetting("Alchemy.uiFolderLocation", "/UI/dist/");
                              
            using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile($"{uiLocationFolder}index.html")))
		    {
		        content = sr.ReadToEnd();
		    }

		    if (string.IsNullOrEmpty(content))
		    {
                masterControls.AddRange(CreateHeaderControls(_securityState));

                masterControls.AddRange(_controls);

                masterControls.AddRange(CreateFooterControls());
            }

            using (var writer = new HtmlTextWriter(response.Output))
            {
                // this securitydisabler allows the control panel to execute unfettered when debug compilation is enabled but you are not signed into Sitecore
                using (new SecurityDisabler())
                {
                    if (masterControls.Any())
                    {
                        foreach (var control in masterControls)
                            control.Render(writer);
                    }
                    else
                    {
                        content = content.Replace("\"/src", $"\"{uiLocationFolder}src");
                        writer.Write(content);
                    }
                }
            }
            response.End();
		}

        protected virtual IEnumerable<IControlPanelControl> CreateHeaderControls(SecurityState securityState)
        {
            yield return new HtmlHeadAndStyles();
        }

        protected virtual IEnumerable<IControlPanelControl> CreateFooterControls()
        {
            yield return new HtmlFooter();
        }
    }
}
