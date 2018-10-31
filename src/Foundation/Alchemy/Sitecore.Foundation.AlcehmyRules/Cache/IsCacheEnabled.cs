using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Foundation.AlchemyBase;

namespace Sitecore.Foundation.AlcehmyRules.Cache
{
    public class IsCacheEnabled	: IAlchemyRule
    {
	    public bool Pass { get; set; }
    }
}
