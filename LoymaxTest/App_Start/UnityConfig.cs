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
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<LoymaxTestBotDbContext>();
            container.RegisterType<IUserDataRepository, DbUserDataRepository>();
            container.RegisterType<IStateRepository, DbStateRepository>();
            container.RegisterType<ITelegramBotClient, TelegramBotClient>(
                new InjectionConstructor(WebConfigurationManager.AppSettings["botToken"], new InjectionParameter<HttpClient>(null)));         
            container.RegisterType<ActionManager>();
#if DEBUG
            container.RegisterType<IActionListFactory, DebugActionListFactory>();
#else
            container.RegisterType<IActionListFactory, DefaultActionListFactory>();
#endif

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}