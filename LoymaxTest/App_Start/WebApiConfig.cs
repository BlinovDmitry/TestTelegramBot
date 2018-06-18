using LoymaxTest.Controllers;
using LoymaxTest.Controllers.Actions;
using LoymaxTest.Models;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Telegram.Bot;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace LoymaxTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API          
            BotApiController.SetupWebHookAsync().Wait();

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
