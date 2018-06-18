using Resources;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers.Actions
{
    public class GetInfoAction : AbstractSimpleAction
    {
        protected override string Command => "getInfo";

        protected override async Task DoHandleMessageAsync(Message message, IMessageContext context)
        {
            var contact = context.UserDataRepository.Find(message.From.Id);

            if (contact == null)
            {
                await ReplyToMessageAsync(message, context, LoymaxTestBotResources.GetInfoActionExecuteErrorNoContact);
            }
            else
            {
                var fullName =$"{contact.LastName} {contact.Name} {contact.MidName}";
                if (fullName.Trim() == "")
                    fullName = LoymaxTestBotResources.RegisterActionExecuteOkNoFullNameSubstring;
                var birthDayStr = contact.BirthDate?.ToShortDateString() 
                    ?? LoymaxTestBotResources.RegisterActionExecuteOkNoBirthdaySubstring;

                await ReplyToMessageAsync(message, context, string.Format(LoymaxTestBotResources.GetInfoActionExecuteOk, fullName, birthDayStr));
            }            
        }
    }
}