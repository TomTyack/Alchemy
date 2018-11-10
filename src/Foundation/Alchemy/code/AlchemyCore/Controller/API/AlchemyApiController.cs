using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Sitecore.Configuration;
using Sitecore.Foundation.Alchemy.Configuration;
using Sitecore.Foundation.Alchemy.Engine;
using Sitecore.Foundation.AlchemyBase.ResponseWrapper;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Sitecore.Foundation.Alchemy.Controller.API
{
    [EnableCors("*", "*", "GET")]
    public class AlchemyApiController : ServicesApiController
    {
        private IDefaultAlchmeyRuleEngine _ruleEngine;
        public AlchemyApiController()
        {
            _ruleEngine = AlchemyConfigurationManager.Configurations.FirstOrDefault(configuration => configuration.Resolve<IDefaultAlchmeyRuleEngine>() != null) as IDefaultAlchmeyRuleEngine;
        }

        [HttpGet]
        //[BasicAuthentication("User", "ssLK")]
        [Route("alchemy/api/rules/run/")]
        public HttpResponseMessage RunRulesEngine([FromUri] string classexternalid)
        {
	        _ruleEngine.Begin();

            return Request.CreateResponse(HttpStatusCode.OK,
                new WebApiResponse("NotAvailable", "The class did not have an introduction text."));
        }

        [HttpGet]
        //[BasicAuthentication("User", "ssLK")]
        [Route("alchemy/api/rules/ruleslist/")]
        public HttpResponseMessage GetRulesList()
        {
            return this.Request.CreateResponse(_ruleEngine.GetRulesList());
        }
    }
}