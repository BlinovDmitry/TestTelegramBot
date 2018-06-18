using System.Collections.Generic;

namespace LoymaxTest.Controllers.Actions
{
    public class DebugActionListFactory : DefaultActionListFactory
    {
        protected override IList<AbstractAction> DoCreateInstance()
        {
            var list = base.DoCreateInstance();
            list.Add(new PingAction());            
            return list;
        }
    }
}