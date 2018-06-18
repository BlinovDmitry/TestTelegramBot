using Resources;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LoymaxTest.Controllers.Actions
{
    public class UnknownAction : AbstractAction
    {
        protected override bool MatchMessage(Message message, IMessageContext context)
        {
            if (message.Type != MessageType.Text)
                return false;
            if (IsInChat(message))
                return Regex.IsMatch(message.Text, $"/.*@{BotName}", RegexOptions.IgnoreCase);
            else
                return Regex.IsMatch(message.Text, "/.*", RegexOptions.IgnoreCase);
        }

        protected override async Task DoHandleMessageAsync(Message message, IMessageContext context)
        {
            await ReplyToMessageAsync(message, context, LoymaxTestBotResources.UnknownActionExecure);
        }
    }


}