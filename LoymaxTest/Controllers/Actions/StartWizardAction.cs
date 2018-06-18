using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers.Actions
{
    public class StartWizardAction : AbstractSimpleAction
    {
        public StartWizardAction(string command, ChainWizardAction startChainAction)
        {
            Command = command;
            ChainStart = startChainAction;
        }

        public ChainWizardAction ChainStart { get; set; }

        protected override string Command { get; }

        public override async Task<bool> HandleMessageAsync(Message message, IMessageContext context)
        {
            if (MatchMessage(message, context))
            {
                if (ChainStart != null)
                    await ChainStart.ApplyStateAsync(message, context);
                return true;
            }
            else if (ChainStart != null && !context.StateRepository.IsStateEmpty(message.Chat.Id, message.From.Id)) 
                return await ChainStart.HandleMessageAsync(message, context);
            return false;
        }        
    }
}