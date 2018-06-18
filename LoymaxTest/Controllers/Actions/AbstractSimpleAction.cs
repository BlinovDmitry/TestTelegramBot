using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LoymaxTest.Controllers.Actions
{
    public abstract class AbstractSimpleAction : AbstractAction
    {
        protected abstract string Command { get; }

        protected override bool MatchMessage(Message message, IMessageContext context)
        {
            return (message.Type == MessageType.Text) 
                && ((string.Compare(message.Text.Trim(), $"/{Command}", true) == 0)
                    || (string.Compare(message.Text.Trim(), $"/{Command}@{BotName}", true) == 0));
        }
    }
}