using System.Linq;
using System.Reflection;

namespace Sitecore.Foundation.RankingFoundry
{
	public static class FoundryVersion
    {
		public static string Current
		{
			get { return ((AssemblyInformationalVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).Single()).InformationalVersion; }
		}
	}
}
