using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.AlchemyBase;
using Sitecore.Foundation.AlchemyBase.ResponseWrapper;

namespace Sitecore.Foundation.Alchemy.Engine
{
	public interface Rules
	{                        
	    List<IAlchemyRule> AlchemyRule { get; set; }
	}                                                                           
}
