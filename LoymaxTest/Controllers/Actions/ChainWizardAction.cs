using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LoymaxTest.Controllers.Actions
{
    public class ChainWizardAction : AbstractAction
    {
        public ChainWizardAction(string text, Guid stateGuid, ChainActioHandler beforeActionHandler = null, ChainActioHandler afterActionHandler = null)
        {
            Text = text;
            StateGuid = stateGuid;
            OnBeforeActionHandler += beforeActionHandler;
            OnAfterActionHandler += afterActionHandler;
        }

        public delegate Task<bool> ChainActioHandler(Message message, IMessageContext context, ChainWizardAction action);

        public event ChainActioHandler OnBeforeActionHandler;

        public event ChainActioHandler OnAfterActionHandler;

        public string Text { get; set; }

        protected Guid StateGuid { get; }

        protected ChainWizardAction NextChainAction { get; set; }

        public async Task ApplyStateAsync(Message message, IMessageContext context)
        {
            if (OnBeforeActionHandler == null || await OnBeforeActionHandler(message, context, this))
            {
                context.StateRepository[message.Chat.Id, message.From.Id] = StateGuid;
                await ReplyToMessageAsync(message, context, Text);
            }
            else
                context.StateRepository.ClearState(message.Chat.Id, message.From.Id);
        }

        public ChainWizardAction AddChainAction(ChainWizardAction chainAction)
        {
            if (NextChainAction == null)
                return NextChainAction = chainAction;
            else
                return NextChainAction.AddChainAction(chainAction);
        }

        protected override bool MatchMessage(Message message, IMessageContext context)
        {
            return (message.Type == MessageType.Text) 
                && context.StateRepository[message.Chat.Id, message.From.Id] == StateGuid;            
        }             
        
        public override async Task<bool> HandleMessageAsync(Message message, IMessageContext context)
        {
            if (MatchMessage(message, context))
            {
                if (await OnAfterActionHandler(message, context, this))
                {
                    if (NextChainAction == null)
                        context.StateRepository.ClearState(message.Chat.Id, message.From.Id);
                    else
                        await NextChainAction.ApplyStateAsync(message, context);                    
                }
                return true;
            }
            else if (NextChainAction != null)
                return await NextChainAction.HandleMessageAsync(message, context);
            return false;
        }
    }
}