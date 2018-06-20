using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoymaxTest.Models
{
    public struct State
    {
        public State(Guid stateGuid, object additionalData)
        {
            StateGuid = stateGuid;
            AdditionalData = additionalData;            
        }

        public static State Empty { get; } = new State(Guid.Empty, null);

        public bool IsEmpty => StateGuid == Guid.Empty;

        public Guid StateGuid { get; set; }

        public object AdditionalData { get; set; }

     }
}