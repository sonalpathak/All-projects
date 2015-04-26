using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Neudesic.Schoolistics.WebRole;
using Neudesic.Schoolistics.WebRole.App_Start;
using System.Web.Mvc;

namespace Neudesic.Schoolistics.WebRole
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            WebApiConfig.RegisterRepositories(GlobalConfiguration.Configuration);

            WebApiConfig.CreateTables();

            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy =
   IncludeErrorDetailPolicy.Always;
        }
    }
}
