using Resources;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers.Actions
{
    public class DeleteAction : AbstractSimpleAction
    {
        protected override string Command => "delete";

        protected override async Task DoHandleMessageAsync(Message message, IMessageContext context)
        {          
            var contact = context.UserDataRepository.Find(message.From.Id);

            if (contact == null)
            {
                await ReplyToMessageAsync(message, context, LoymaxTestBotResources.DeleteActionExecuteErrorNoContact);
            }
            else
            {
                context.UserDataRepository.Remove(contact);
                await context.UserDataRepository.SaveChangesAsync();

                await ReplyToMessageAsync(message, context, LoymaxTestBotResources.DeleteActionExecuteOk);
            }            
        }
    }
}