using Resources;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers.Actions
{
    public class PingAction : AbstractSimpleAction
    {
        protected override string Command => "ping";

        protected override async Task DoHandleMessageAsync(Message message, IMessageContext context)
        {
            await ReplyToMessageAsync(message, context, LoymaxTestBotResources.PingActionExecuteOk);
        }
    }
}