using LoymaxTest.Models;
using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers.Actions
{
    public abstract class AbstractAction
    {
        protected string BotName { get; } = WebConfigurationManager.AppSettings["botName"];

        public bool IsInChat(Message message)
        {
            return message.Chat.Id != message.From.Id;
        }

        public virtual async Task ReplyToMessageAsync(Message message, IMessageContext context, string replyText)
        {            
            await context.TelegramBotApiClient.SendTextMessageAsync(message.Chat.Id, replyText, replyToMessageId: (IsInChat(message) ? message.MessageId : 0));
        }

        protected virtual Task DoHandleMessageAsync(Message message, IMessageContext context)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> HandleMessageAsync(Message message, IMessageContext context)
        {
            if (MatchMessage(message, context))
            {                
                await DoHandleMessageAsync(message, context);
                await context.StateRepository.ClearStateAsync(message.Chat.Id, message.From.Id);
                return true;
            }
            return false;
        }

        protected abstract bool MatchMessage(Message message, IMessageContext context);
    }
}