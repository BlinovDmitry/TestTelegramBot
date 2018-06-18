using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers.Actions
{
    public class ActionManager
    {
        public ActionManager(IActionListFactory actionListFactory)
        {
            Actions = actionListFactory.CreateInstance();
        }

        protected IList<AbstractAction> Actions { get; }                

        public async Task HandleMessageAsync(Message message, IMessageContext context)
        {
            foreach (var action in Actions)
                if (await action.HandleMessageAsync(message, context))
                    break;
        }
    }
}