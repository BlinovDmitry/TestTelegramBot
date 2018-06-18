using System.Collections.Generic;

namespace LoymaxTest.Controllers.Actions
{
    public interface IActionListFactory
    {
        IList<AbstractAction> CreateInstance();
    }
}
