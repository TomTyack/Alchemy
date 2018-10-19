using System.Linq;
using System.Reflection;

namespace Sitecore.Foundation.Alchemy
{
	public static class AlchemyVersion
    {
		public static string Current
		{
			get { return ((AssemblyInformationalVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).Single()).InformationalVersion; }
		}
	}
}
