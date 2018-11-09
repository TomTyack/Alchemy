using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Sitecore.Foundation.Alchemy.Configuration
{
    public static class XmlActivator
    {
        public static T CreateObject<T>(XmlNode node) where T : class
        {
            string innerText = node.Attributes?["type"]?.InnerText;
            if (innerText == null)
                return default(T);
            Type type = Type.GetType(innerText);
            if (type == (Type)null)
                return default(T);
            return Activator.CreateInstance(type) as T;
        }
    }
}