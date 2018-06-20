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
        private const string tokenParamName = "token";

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
            await telegramBotApiClient.DeleteWebhookAsync();
            await telegramBotApiClient.SetWebhookAsync($"{botUrl}?{tokenParamName}={BotToken}");
        }

        [Route(updateHookRoute)]        
        [HttpPost]
        [HttpGet]
        public async Task<IHttpActionResult> PostUpdate([FromUri(Name=tokenParamName)] string token, [FromBody] Update update)
        {            
            if ((update?.Message != null) && (token == BotToken))
                await ActionManager.HandleMessageAsync(update.Message, this);
            return Ok();         
        }
    }
}
