using LoymaxTest.Controllers;
using LoymaxTest.Models;
using System.Net;
using System.Web.Http;
using Telegram.Bot;

namespace LoymaxTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {        
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
        }
    }
}
