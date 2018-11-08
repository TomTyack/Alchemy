using System.Web.UI;

namespace Sitecore.Foundation.Alchemy.ControlPanel.Controls
{
	internal class HtmlFooter : IControlPanelControl
	{
		public void Render(HtmlTextWriter writer)
		{
            //  https://github.com/filamentgroup/loadJS/blob/master/loadJS.js
            writer.Write(@"<script>	/*!loadJS: load a JS file asynchronously. [c]2014 @scottjehl, Filament Group, Inc. (Based on http://goo.gl/REQGQ by Paul Irish). Licensed MIT */
		    !function (e) { var t = function (t, n) { 'use strict'; var o = e.document.getElementsByTagName('script')[0], r = e.document.createElement('script'); return r.src = t, r.async = !0, o.parentNode.insertBefore(r, o), n && 'function' == typeof n && (r.onload = n), r }; 'undefined' != typeof module ? module.exports = t : e.loadJS = t }('undefined' != typeof global ? global : this);
            </script>");

            // 
		    writer.Write("<script>");
		    writer.Write("var requireJs = 'https://cdnjs.cloudflare.com/ajax/libs/require.js/2.3.6/require.min.js';var reactScriptsArray = ['https://cdnjs.cloudflare.com/ajax/libs/react/16.4.2/cjs/react.development.js'];");
		    writer.Write("for (var scriptsLoaded = 0, i = 0; i < reactScriptsArray.length; i++)loadJS(reactScriptsArray[i], function() { scriptsLoaded === reactScriptsArray.length - 1 && startNext(), scriptsLoaded++ });");
           

		    writer.Write("function startNext(){");
		    writer.Write("loadJS(requireJs, function() { console.log(\"react bootup\")  })");

            writer.Write("}");
		    writer.Write("</script>");
            writer.Write(" </body></html>");
		}
	}
}
