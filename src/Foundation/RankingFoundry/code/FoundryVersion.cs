﻿using System.Linq;
using System.Reflection;

namespace Unicorn
{
	public static class FoundryVersion
    {
		public static string Current
		{
			get { return ((AssemblyInformationalVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).Single()).InformationalVersion; }
		}
	}
}
