using LoymaxTest.Controllers.Actions;
using LoymaxTest.Models;
using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers
{
    public class BotApiController : ApiController, IMessageContext
    {       
        private const string updateHookRoute = "api/v1/update";
               
        public BotApiController(IUserDataRepository userDataStorage, IStateRepository stateStorage, ITelegramBotClient telegramBotApiClient, ActionManager actionManager)
        {
            UserDataRepository = userDataStorage;
            StateRepository = stateStorage;
            TelegramBotApiClient = telegramBotApiClient;
            ActionManager = actionManager;
        }        

        public ITelegramBotClient TelegramBotApiClient { get; }

        public IStateRepository StateRepository { get; }

        public IUserDataRepository UserDataRepository { get; }

        protected ActionManager ActionManager { get; }

        protected static string BotToken { get; } = WebConfigurationManager.AppSettings["botToken"];

        protected static Uri BotHostUrl { get; } = new Uri(WebConfigurationManager.AppSettings["botHostUrl"]);

        public async static Task SetupWebHookAsync()
        {
            var telegramBotApiClient = new TelegramBotClient(BotToken);

            var botUrl = new Uri(BotHostUrl, updateHookRoute);
                        
            await telegramBotApiClient.SetWebhookAsync($"{botUrl.ToString()}?token={BotToken}");
        }

        [Route(updateHookRoute)]        
        [HttpPost]        
        public async Task<IHttpActionResult> PostUpdate(string token, [FromBody] Update update)
        {            
            if ((update?.Message != null) && (token == WebConfigurationManager.AppSettings["botToken"]))
                await ActionManager.HandleMessageAsync(update.Message, this);
            return Ok();         
        }
    }
}
